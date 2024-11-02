import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const Cart = () => {
  const [cart, setCart] = useState(null);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  // Fetch cart details from the API when the component mounts
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

  // Handle removing a ticket from the cart
  const handleRemoveTicket = async (ticketId) => {
    try {
      const cartId = localStorage.getItem('cartId');
      await axios.delete('/api/Cart/RemoveTicketFromCart', {
        data: { cartId, ticketId },
      });
      setCart((prevCart) => ({
        ...prevCart,
        tickets: prevCart.tickets.filter((ticket) => ticket.id !== ticketId),
      }));
    } catch (error) {
      setError('Failed to remove the ticket. Please try again.');
    }
  };

  // Handle proceeding to checkout
  const handleCheckout = () => {
    navigate('/checkout');
  };

  if (!cart) {
    return <div>Loading cart...</div>;
  }

  return (
    <div>
      <h1>Your Cart</h1>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <div>
        {cart.tickets.map((ticket) => (
          <div
            key={ticket.id}
            style={{ borderBottom: '1px solid #ccc', marginBottom: '10px' }}
          >
            <p>Movie: {ticket.showtime.movie.title}</p>
            <p>Showtime: {new Date(ticket.showtime.startTime).toLocaleString()}</p>
            <p>Price: ${ticket.price}</p>
            <button
              onClick={() => handleRemoveTicket(ticket.id)}
              style={buttonStyle}
            >
              Remove Ticket
            </button>
          </div>
        ))}
      </div>
      <h3>Total: ${cart.totalPrice}</h3>
      {/* Checkout button at the bottom */}
      <div style={{ marginTop: '20px', textAlign: 'center' }}>
        <button onClick={handleCheckout} style={buttonStyle}>
          Proceed to Checkout
        </button>
      </div>
    </div>
  );
};

// Button styling
const buttonStyle = {
  padding: '10px 20px',
  backgroundColor: '#4CAF50',
  color: 'white',
  border: 'none',
  borderRadius: '5px',
  cursor: 'pointer',
};

export default Cart;