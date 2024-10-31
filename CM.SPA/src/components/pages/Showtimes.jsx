import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getShowtimes } from '../../Services/showtimeService';
import { getMovies } from '../../Services/movieService';
import {
  Container,
  Typography,
  Card,
  CardContent,
  List,
  ListItem,
  Button,
  CircularProgress,
  Box,
} from '@mui/material';

export default function Showtimes() {
  const { movieId } = useParams();
  const navigate = useNavigate();
  const [showtimes, setShowtimes] = useState([]);
  const [movie, setMovie] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchShowtimes = async () => {
      try {
        const showtimesData = await getShowtimes(movieId);
        setShowtimes(showtimesData);
      } catch (error) {
        console.error('Failed to fetch showtimes:', error);
      }
    };

    const fetchMovie = async () => {
      try {
        const moviesData = await getMovies();
        const movie = moviesData.find(
          (movie) => movie.id === parseInt(movieId),
        );
        if (movie) {
          setMovie(movie);
        }
      } catch (error) {
        console.error('Failed to fetch movie:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchShowtimes();
    fetchMovie();
  }, [movieId]);

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

  if (!movie) {
    return (
      <Typography variant="h6" align="center">
        Movie not found
      </Typography>
    );
  }

  const handleShowtimeClick = (showtimeId) => {
    navigate(`/tickets/${showtimeId}`);
  };

  return (
    <Container maxWidth="md" sx={{ mt: 4 }}>
      <Card sx={{ mb: 4 }}>
        <CardContent>
          <Typography variant="h4" gutterBottom>
            Showtimes for {movie.title}
          </Typography>
          <Typography variant="body1" color="textSecondary" paragraph>
            <strong>Description:</strong> {movie.description}
          </Typography>
          <Typography variant="body1" color="textSecondary">
            <strong>Rating:</strong> {movie.rating}
          </Typography>
          <Typography variant="body1" color="textSecondary">
            <strong>Date Released:</strong>{' '}
            {new Date(movie.dateReleased).toLocaleDateString()}
          </Typography>
        </CardContent>
      </Card>

      <Typography variant="h5" gutterBottom>
        Available Showtimes
      </Typography>
      <List>
        {showtimes.map((showtime) => (
          <ListItem key={showtime.id} sx={{ pl: 0 }}>
            <Button
              variant="contained"
              onClick={() => handleShowtimeClick(showtime.id)}
            >
              {new Date(showtime.startTime).toLocaleString('en-US', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: 'numeric',
                minute: 'numeric',
                hour12: true,
              })}
            </Button>
            <Typography variant="body2" color="textSecondary" sx={{ ml: 2 }}>
              Tickets Available: {showtime.availableTickets}
            </Typography>
            <Typography variant="body2" color="textSecondary" sx={{ ml: 2 }}>
              Ticket Price: $
              {typeof showtime.ticketPrice === 'number'
                ? showtime.ticketPrice.toFixed(2)
                : 'N/A'}
            </Typography>
          </ListItem>
        ))}
      </List>
    </Container>
  );
}
