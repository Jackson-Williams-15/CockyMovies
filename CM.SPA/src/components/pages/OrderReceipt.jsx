import React, { useEffect, useState } from 'react';
import axios from 'axios';

const OrderReceipt = ({ orderId }) => {
  const [order, setOrder] = useState(null);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchOrderDetails = async () => {
      try {
        const response = await axios.get(`/api/Order/${orderId}`);
        setOrder(response.data);
      } catch (error) {
        setError('Failed to fetch order details.');
      }
    };

    fetchOrderDetails();
  }, [orderId]);

  if (error) {
    return <div>{error}</div>;
  }

  if (!order) {
    return <div>Loading...</div>;
  }

  return (
    <div>
      <h2>Order Receipt</h2>
      <p>Order ID: {order.id}</p>
      <p>Processed Date: {new Date(order.processedDate).toLocaleString()}</p>
      <p>Total Price: ${order.totalPrice.toFixed(2)}</p>
      <h3>Tickets</h3>
      <ul>
        {order.tickets.map(ticket => (
          <li key={ticket.orderTicketId}>
            <p>Showtime: {new Date(ticket.showtime.startTime).toLocaleString()}</p>
            <p>Movie: {ticket.movie.title}</p>
            <p>Price: ${ticket.price.toFixed(2)}</p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default OrderReceipt;