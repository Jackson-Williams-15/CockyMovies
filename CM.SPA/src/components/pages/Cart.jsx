import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const Cart = ({ cartId }) => {
  const [cart, setCart] = useState(null);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  // Fetch cart details from the API when the component mounts
  useEffect(() => {
    const fetchCart = async () => {
      try {
        const response = await axios.get(`/api/Cart/GetCartById/${cartId}`);
        setCart(response.data);
      } catch (error) {
        setError('Failed to load cart. Please try again.');
      }
    };

    fetchCart();
  }, [cartId]);

  // Handle removing a ticket from the cart
  const handleRemoveTicket = async (ticketId) => {
    try {
      const response = await axios.put('/api/Cart/RemoveTicketFromCart', {
        cartId,
        ticketId,
      });
      setCart(response.data.cartDetails);
    } catch (error) {
      setError('Failed to remove the ticket. Please try again.');
    }
  };

  // Handle proceeding to checkout
  const handleCheckout = () => {
    navigate('/checkout'); // Navigate to the checkout page
  };

  if (!cart) {
    return <div>Loading cart...</div>;
  }

  return (
    <div>
      <h1>Your Cart</h1>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      
      <div>
        {cart.Tickets.map((ticket) => (
          <div key={ticket.Id} style={{ borderBottom: '1px solid #ccc', marginBottom: '10px' }}>
            <p>Showtime: {ticket.Showtime.Name}</p>
            <p>Price: ${ticket.Price}</p>
            <button onClick={() => handleRemoveTicket(ticket.Id)} style={buttonStyle}>
              Remove Ticket
            </button>
          </div>
        ))}
      </div>

      <h3>Total: ${cart.TotalPrice}</h3>

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
