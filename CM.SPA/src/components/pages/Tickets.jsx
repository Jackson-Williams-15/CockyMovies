import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { Button, Typography, CircularProgress, Box } from '@mui/material';

const Tickets = () => {
  const { showtimeId } = useParams();
  const [showtime, setShowtime] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [cartId, setCartId] = useState(null);

  useEffect(() => {
    const fetchShowtime = async () => {
      try {
        const response = await axios.get(`/api/Showtimes/${showtimeId}`);
        setShowtime(response.data);
      } catch (error) {
        setError('Failed to load showtime. Please try again.');
      } finally {
        setLoading(false);
      }
    };

    const fetchCartId = async () => {
      try {
        const response = await axios.get('/api/Account/profile', {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        });
        setCartId(response.data.cartId);
      } catch (error) {
        setError('Failed to load user cart. Please try again.');
      }
    };

    fetchShowtime();
    fetchCartId();
  }, [showtimeId]);

  const handleAddTicketToCart = async (ticketId) => {
    try {
      await axios.post('/api/Cart/AddTicketToCart', {
        cartId,
        ticketId,
      });
      alert('Ticket added to cart successfully.');
    } catch (error) {
      setError('Failed to add ticket to cart. Please try again.');
    }
  };

  if (loading) {
    return (
      <Box
        sx={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          height: '100vh',
        }}
      >
        <CircularProgress />
      </Box>
    );
  }

  if (error) {
    return (
      <Typography variant="h6" color="error">
        {error}
      </Typography>
    );
  }

  if (!showtime) {
    return <Typography variant="h6">Showtime not found</Typography>;
  }

  return (
    <div>
      <Typography variant="h4">Showtime Details</Typography>
      <Typography variant="body1">Movie: {showtime.movie.title}</Typography>
      <Typography variant="body1">
        Start Time: {new Date(showtime.startTime).toLocaleString()}
      </Typography>
      <Typography variant="body1">
        Available Tickets: {showtime.tickets.length}
      </Typography>
      <Typography variant="body1">
        Ticket Price: ${showtime.tickets[0]?.price.toFixed(2)}
      </Typography>
      <Button
        variant="contained"
        onClick={() => handleAddTicketToCart(showtime.tickets[0]?.id)}
      >
        Add Ticket to Cart
      </Button>
    </div>
  );
};

export default Tickets;
