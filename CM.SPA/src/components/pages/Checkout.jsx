import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import {
  Container,
  Box,
  Typography,
  Paper,
  TextField,
  Button,
  Divider,
  List,
  ListItem,
  CircularProgress,
  Alert,
} from '@mui/material';

const Checkout = () => {
  const [cart, setCart] = useState(null);
  const [error, setError] = useState('');
  const [paymentDetails, setPaymentDetails] = useState({
    cardNumber: '',
    expiryDate: '',
    cvv: '',
    cardHolderName: '',
  });
  const navigate = useNavigate();

  const fetchCart = async () => {
    try {
      const cartId = localStorage.getItem('cartId');
      const response = await axios.get(`/api/Cart/GetCartById/${cartId}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });
      setCart(response.data);
      localStorage.setItem('cart', JSON.stringify(response.data)); // Update local storage
      setError('');
      updateUserLocalStorage(response.data); // Update user local storage
    } catch (error) {
      setError('Failed to load cart. Please try again.');
    }
  };

  const updateUserLocalStorage = (cartData) => {
    const user = JSON.parse(localStorage.getItem('user'));
    if (user) {
      user.cart = cartData;
      localStorage.setItem('user', JSON.stringify(user));
    }
  };

  const fetchPaymentDetails = async () => {
    try {
      const response = await axios.get('/api/Account/profile', {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });
      const user = response.data;
      if (user.paymentDetails) {
        setPaymentDetails({
          cardNumber: user.paymentDetails.cardNumber,
          expiryDate: user.paymentDetails.expiryDate,
          cvv: user.paymentDetails.cvv,
          cardHolderName: user.paymentDetails.cardHolderName,
        });
      }
      setError('');
    } catch (error) {
      setError('Failed to load payment details. Please try again.');
    }
  };

  useEffect(() => {
    fetchCart();
    fetchPaymentDetails();
  }, []);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setPaymentDetails((prevDetails) => ({
      ...prevDetails,
      [name]: value,
    }));
  };

  const handleCheckout = async (e) => {
    e.preventDefault();

    if (!validatePaymentDetails()) {
      setError('Invalid payment details.');
      return;
    }

    try {
      const cartId = parseInt(localStorage.getItem('cartId'), 10);
      const userIdStr = localStorage.getItem('userId');
      if (!userIdStr) {
        setError('User ID is not found in localStorage.');
        return;
      }
      const userId = parseInt(userIdStr, 10);
      if (isNaN(userId)) {
        setError('User ID is invalid.');
        return;
      }

      const response = await axios.post(
        '/api/Checkout/ProcessCheckout',
        {
          cartId,
          userId,
          requestDate: new Date().toISOString(),
          paymentDetails,
        },
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        },
      );

      if (response.status === 200) {
        const orderId = response.data.orderId;
        localStorage.removeItem('cart'); // Clear cart from local storage after checkout
        navigate(`/order-success/${orderId}`);
      } else {
        setError('Checkout failed. Please try again.');
      }
    } catch (error) {
      setError('Checkout failed. Please try again.');
    }
  };

  const validatePaymentDetails = () => {
    const { cardNumber, expiryDate, cvv } = paymentDetails;

    if (!/^\d{16}$/.test(cardNumber)) {
      return false;
    }

    if (!/^\d{3,4}$/.test(cvv)) {
      return false;
    }

    if (!/^(0[1-9]|1[0-2])\/?([0-9]{2})$/.test(expiryDate)) {
      return false;
    }

    return true;
  };

  const groupTicketsByShowtime = (tickets) => {
    const groupedTickets = tickets.reduce((acc, ticket) => {
      const showtimeId = ticket.showtime.id;
      const ticketKey = `${showtimeId}-${ticket.price}`;
      if (!acc[ticketKey]) {
        acc[ticketKey] = { ...ticket, quantity: 0, totalPrice: 0 };
      }
      acc[ticketKey].quantity += ticket.quantity;
      acc[ticketKey].totalPrice += ticket.price * ticket.quantity;
      return acc;
    }, {});
    return Object.values(groupedTickets);
  };

  if (!cart) {
    return (
      <Box
        display="flex"
        justifyContent="center"
        alignItems="center"
        height="100vh"
      >
        <CircularProgress />
      </Box>
    );
  }

  const groupedTickets = groupTicketsByShowtime(cart.tickets);
  const overallTotalPrice = groupedTickets.reduce(
    (acc, ticket) => acc + ticket.totalPrice,
    0,
  );

  return (
    <Container maxWidth="md" sx={{ mt: 4 }}>
      <Paper elevation={3} sx={{ p: 4 }}>
        <Typography variant="h4" gutterBottom>
          Checkout
        </Typography>
        {error && (
          <Alert severity="error" sx={{ mb: 2 }}>
            {error}
          </Alert>
        )}

        <Box mb={4}>
          <Typography variant="h5" gutterBottom>
            Your Cart
          </Typography>
          <List>
            {groupedTickets.map((item) => (
              <ListItem
                key={item.id}
                sx={{ display: 'flex', justifyContent: 'space-between' }}
              >
                <Box>
                  <Typography variant="subtitle1">
                    {item.showtime.movie.title}
                  </Typography>
                  <Typography variant="body2" color="text.secondary">
                    Showtime:{' '}
                    {new Date(item.showtime.startTime).toLocaleString()}
                  </Typography>
                </Box>
                <Typography variant="subtitle1">
                  {item.quantity} x ${item.price}
                </Typography>
              </ListItem>
            ))}
          </List>
          <Divider sx={{ my: 2 }} />
          <Typography variant="h6" align="right">
            Total: ${overallTotalPrice.toFixed(2)}
          </Typography>
        </Box>

        <form onSubmit={handleCheckout}>
          <Typography variant="h5" gutterBottom>
            Payment Information
          </Typography>
          <TextField
            fullWidth
            label="Card Number"
            variant="outlined"
            name="cardNumber"
            value={paymentDetails.cardNumber}
            onChange={handleChange}
            sx={{ mb: 2 }}
            required
          />
          <Box display="flex" gap={2} mb={2}>
            <TextField
              label="Expiry Date"
              variant="outlined"
              name="expiryDate"
              value={paymentDetails.expiryDate}
              onChange={handleChange}
              placeholder="MM/YY"
              fullWidth
              required
            />
            <TextField
              label="CVV"
              variant="outlined"
              name="cvv"
              value={paymentDetails.cvv}
              onChange={handleChange}
              fullWidth
              required
            />
          </Box>
          <TextField
            fullWidth
            label="Card Holder Name"
            variant="outlined"
            name="cardHolderName"
            value={paymentDetails.cardHolderName}
            onChange={handleChange}
            sx={{ mb: 2 }}
            required
          />
          <Button type="submit" variant="contained" color="primary" fullWidth>
            Complete Checkout
          </Button>
        </form>
      </Paper>
    </Container>
  );
};

export default Checkout;