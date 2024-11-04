import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import {
  Container,
  Box,
  Typography,
  Paper,
  Button,
  Divider,
  Alert,
  CircularProgress,
} from '@mui/material';

const Cart = () => {
  const [cart, setCart] = useState(null);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchCart = async () => {
      try {
        const cartId = localStorage.getItem('cartId');
        const response = await axios.get(`/api/Cart/GetCartById/${cartId}`, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        });
        setCart(response.data);
      } catch (error) {
        setError('Failed to load cart. Please try again.');
      }
    };
    fetchCart();
  }, []);

  const handleRemoveTicket = async (ticketId) => {
    try {
      const cartId = localStorage.getItem('cartId');
      await axios.delete('/api/Cart/RemoveTicketFromCart', {
        params: { cartId, ticketId },
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });

      setCart((prevCart) => {
        const updatedTickets = prevCart.tickets
          .map((ticket) => {
            if (ticket.id === ticketId) {
              return {
                ...ticket,
                quantity: ticket.quantity - 1,
              };
            }
            return ticket;
          })
          .filter((ticket) => ticket.quantity > 0);

        return {
          ...prevCart,
          tickets: updatedTickets,
        };
      });
    } catch (error) {
      setError('Failed to remove the ticket. Please try again.');
    }
  };

  const handleCheckout = () => {
    navigate('/checkout');
  };

  const groupTicketsByShowtime = (tickets) => {
    const groupedTickets = tickets.reduce((acc, ticket) => {
      const showtimeId = ticket.showtime.id;
      const ticketKey = `${showtimeId}-${ticket.price}`;
      if (!acc[ticketKey]) {
        acc[ticketKey] = {
          ...ticket,
          quantity: 0,
          totalPrice: 0,
        };
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
    <Container maxWidth="sm" sx={{ mt: 4, px: 3 }}>
      <Typography variant="h4" gutterBottom>
        Your Cart
      </Typography>
      {error && (
        <Alert severity="error" sx={{ mb: 2 }}>
          {error}
        </Alert>
      )}

      {/* Scrollable container for tickets */}
      <Box
        sx={{
          maxHeight: 400,
          overflowY: 'auto',
          mb: 4,
          pr: 1
        }}
      >
        {groupedTickets.map((ticket) => (
          <Paper
            key={`${ticket.showtime.id}-${ticket.price}`}
            sx={{ p: 2, mb: 2 }}
          >
            <Box display="flex" justifyContent="space-between">
              <Box>
                <Typography variant="subtitle1">
                  {ticket.showtime.movie.title}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Showtime:{' '}
                  {new Date(ticket.showtime.startTime).toLocaleString()}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Quantity: {ticket.quantity}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                  Price per Ticket: ${ticket.price}
                </Typography>
                <Typography variant="subtitle1" sx={{ mt: 1 }}>
                  Total Price: ${ticket.totalPrice.toFixed(2)}
                </Typography>
              </Box>
              <Button
                variant="contained"
                color="secondary"
                onClick={() => handleRemoveTicket(ticket.id)}
              >
                Remove Ticket
              </Button>
            </Box>
          </Paper>
        ))}
      </Box>

      <Divider />
      <Box display="flex" justifyContent="space-between" mt={2}>
        <Typography variant="h5">
          Total: ${overallTotalPrice.toFixed(2)}
        </Typography>
        <Button
          variant="contained"
          color="primary"
          onClick={handleCheckout}
          sx={{ mt: 2 }}
        >
          Proceed to Checkout
        </Button>
      </Box>
    </Container>
  );
};

export default Cart;
