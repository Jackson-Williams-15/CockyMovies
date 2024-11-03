import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import axios from 'axios';
import {
  Button,
  Typography,
  CircularProgress,
  Box,
  TextField,
  AlertTitle,
} from '@mui/material';
import Alert from '@mui/material/Alert';

const Tickets = () => {
  const { showtimeId } = useParams();
  const [showtime, setShowtime] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [quantity, setQuantity] = useState(1);
  const [alert, setAlert] = useState({
    show: false,
    severity: '',
    message: '',
  });

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

    fetchShowtime();
  }, [showtimeId]);

  const handleAddTicketToCart = async (quantity) => {
    try {
      const cartId = localStorage.getItem('cartId');
      const ticketIds = showtime.tickets
        .slice(0, quantity)
        .map((ticket) => ticket.id);
      await axios.post(
        '/api/Cart/AddTicketToCart',
        {
          cartId,
          ticketId: ticketIds,
          quantity,
        },
        {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        },
      );
      setAlert({
        show: true,
        severity: 'success',
        message: 'Your tickets were added to your cart.',
      });
    } catch (error) {
      setAlert({
        show: true,
        severity: 'error',
        message: 'There was an error adding your tickets.',
      });
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
    <Box
      sx={{
        padding: (theme) => theme.spacing(3), // Adds responsive padding using theme spacing
        maxWidth: 'md', // Limits width without hardcoding
        margin: '0 auto', // Centers the container horizontally
        display: 'flex',
        flexDirection: 'column',
        gap: 2, // Adds gap between elements for consistent spacing
      }}
    >
      <Typography variant="h4">Showtime Details</Typography>
      <Typography variant="body1">Movie: {showtime.movie.title}</Typography>
      <Typography variant="body1">
        Start Time: {new Date(showtime.startTime).toLocaleString()}
      </Typography>
      <Typography variant="body1">
        Available Tickets: {showtime.availableTickets}
      </Typography>
      <Typography variant="body1">
        Ticket Price: ${showtime.tickets[0]?.price.toFixed(2)}
      </Typography>
      <TextField
        label="Quantity"
        type="number"
        value={quantity}
        onChange={(e) => setQuantity(parseInt(e.target.value))}
        inputProps={{ min: 1, max: showtime.tickets.length }}
        sx={{ mb: 2 }}
      />
      <Button
        variant="contained"
        onClick={() => handleAddTicketToCart(quantity)}
      >
        Add Tickets to Cart
      </Button>
      {alert.show && (
        <Box
          sx={{
            marginTop: 2,
            width: '100%', // Full width for alignment within container
            display: 'flex',
            justifyContent: 'center', // Centers the alert within the container
          }}
        >
          <Alert
            severity={alert.severity}
            onClose={() => setAlert({ ...alert, show: false })}
          >
            <AlertTitle>
              {alert.severity === 'success' ? 'Success' : 'Error'}
            </AlertTitle>
            {alert.message}
          </Alert>
        </Box>
      )}
    </Box>
  );
};

export default Tickets;
