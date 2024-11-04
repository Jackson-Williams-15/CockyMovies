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

  // Handle proceeding to checkout
  const handleCheckout = () => {
    navigate('/checkout');
  };

  // Group tickets by showtime
  const groupTicketsByShowtime = (tickets) => {
    const groupedTickets = tickets.reduce((acc, ticket) => {
      const showtimeId = ticket.showtime.id;
      // Use showtimeId and price as key
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
    <div>
      <h1>Your Cart</h1>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <div>
        {groupedTickets.map((ticket) => (
          <div
            key={`${ticket.showtime.id}-${ticket.price}`}
            style={{ borderBottom: '1px solid #ccc', marginBottom: '10px' }}
          >
            <p>Movie: {ticket.showtime.movie.title}</p>
            <p>
              Showtime: {new Date(ticket.showtime.startTime).toLocaleString()}
            </p>
            <p>Price: ${ticket.price}</p>
            <p>Quantity: {ticket.quantity}</p>
            <p>Total Price: ${ticket.totalPrice.toFixed(2)}</p>
            <button
              onClick={() => handleRemoveTicket(ticket.id)}
              style={buttonStyle}
            >
              Remove Ticket
            </button>
          </div>
        ))}
      </div>
      <h3>Total: ${overallTotalPrice.toFixed(2)}</h3>
      {/* Checkout button at the bottom */}
      <div style={{ marginTop: '20px', textAlign: 'center' }}>
        <button onClick={handleCheckout} style={buttonStyle}>
          Proceed to Checkout
        </button>
      </div>
    </div>
  );
};

const buttonStyle = {
  padding: '10px 20px',
  backgroundColor: '#4CAF50',
  color: 'white',
  border: 'none',
  borderRadius: '5px',
  cursor: 'pointer',
};

export default Cart;
