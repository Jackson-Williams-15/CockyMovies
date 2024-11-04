import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Container, Box, Typography, Paper, List, ListItem, CircularProgress, Alert } from '@mui/material';

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
    return <Alert severity="error">{error}</Alert>;
  }

  if (!order) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" height="100vh">
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Container maxWidth="sm" sx={{ mt: 4 }}>
      <Paper elevation={3} sx={{ p: 4 }}>
        <Typography variant="h4" component="h1" gutterBottom>
          Order Receipt
        </Typography>
        <Typography variant="body1" color="text.secondary" gutterBottom>
          Order ID: {order.id}
        </Typography>
        <Typography variant="body1" color="text.secondary" gutterBottom>
          Processed Date: {new Date(order.processedDate).toLocaleString()}
        </Typography>
        <Typography variant="h6" color="primary" sx={{ mt: 2 }}>
          Total Price: ${order.totalPrice.toFixed(2)}
        </Typography>

        <Box sx={{ mt: 3 }}>
          {/*<Typography variant="h5" component="h2" gutterBottom>
            Tickets
          </Typography>
          <List>
            {order.tickets.map((ticket) => (
              <ListItem key={ticket.orderTicketId} sx={{ display: 'block', mb: 2 }}>
                <Typography variant="subtitle1">Showtime: {new Date(ticket.showtime.startTime).toLocaleString()}</Typography>
                <Typography variant="subtitle1">Movie: {ticket.movie.title}</Typography>
                <Typography variant="subtitle1">Price: ${ticket.price.toFixed(2)}</Typography>
              </ListItem>
            ))}
          </List>*/}
        </Box>
      </Paper>
    </Container>
  );
};

export default OrderReceipt;
