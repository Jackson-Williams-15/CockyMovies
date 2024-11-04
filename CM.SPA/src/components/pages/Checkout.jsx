import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

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
      const userIdStr = localStorage.getItem('id');

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

      if (response.data.success) {
        const orderId = response.data.orderId;
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
    return <div>Loading cart...</div>;
  }

  const groupedTickets = groupTicketsByShowtime(cart.tickets);
  const overallTotalPrice = groupedTickets.reduce(
    (acc, ticket) => acc + ticket.totalPrice,
    0,
  );

  return (
    <div className="checkout-page">
      <h2>Checkout</h2>
      <div className="cart-items">
        <h3>Your Cart</h3>
        {groupedTickets.map((item) => (
          <div key={item.id} className="cart-item">
            <span>{item.showtime.movie.title}</span>
            <span>
              {item.quantity} x ${item.price}
            </span>
          </div>
        ))}
      </div>

      <div className="total-amount">
        <h3>Total: ${overallTotalPrice.toFixed(2)}</h3>
      </div>

      <form onSubmit={handleCheckout} className="payment-form">
        <h3>Payment Information</h3>
        <label>
          Card Number
          <input
            type="text"
            name="cardNumber"
            value={paymentDetails.cardNumber}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          Expiry Date
          <input
            type="text"
            name="expiryDate"
            value={paymentDetails.expiryDate}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          CVV
          <input
            type="text"
            name="cvv"
            value={paymentDetails.cvv}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          Card Holder Name
          <input
            type="text"
            name="cardHolderName"
            value={paymentDetails.cardHolderName}
            onChange={handleChange}
            required
          />
        </label>
        <button type="submit">Place Order</button>
      </form>
      {error && <p style={{ color: 'red' }}>{error}</p>}
    </div>
  );
};

export default Checkout;
