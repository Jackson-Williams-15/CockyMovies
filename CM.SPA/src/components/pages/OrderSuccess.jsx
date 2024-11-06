import React from 'react';
import { useParams } from 'react-router-dom';
import { Container, Typography, Box } from '@mui/material';
import OrderReceipt from './OrderReceipt';

export default function OrderSuccess() {
  const { orderId } = useParams();

  return (
    <Container maxWidth="sm" sx={{ mt: 4 }}>
      <Box textAlign="center" mb={4}>
        <Typography
          variant="h3"
          component="h1"
          gutterBottom
          color="success.main"
        >
          Order Success
        </Typography>
        <Typography variant="body1" color="text.secondary">
          Your order was processed successfully!
        </Typography>
      </Box>
      <OrderReceipt orderId={orderId} />
    </Container>
  );
}
