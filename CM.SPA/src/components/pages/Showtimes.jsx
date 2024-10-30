import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { getShowtimes } from '../../Services/showtimeService';
import { getMovies } from '../../Services/movieService';
import {
  Container,
  Typography,
  Card,
  CardContent,
  List,
  ListItem,
  ListItemText,
} from '@mui/material';

export default function Showtimes() {
  const { movieId } = useParams();
  const [showtimes, setShowtimes] = useState([]);
  const [movie, setMovie] = useState(null);

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
      }
    };

    fetchShowtimes();
    fetchMovie();
  }, [movieId]);

  if (!movie) {
    return (
      <Typography variant="h6" align="center">
        Movie not found
      </Typography>
    );
  }

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
            <ListItemText
              primary={new Date(showtime.startTime).toLocaleString('en-US', {
                year: 'numeric',
                month: 'long',
                day: 'numeric',
                hour: 'numeric',
                minute: 'numeric',
                hour12: true,
              })}
            />
          </ListItem>
        ))}
      </List>
    </Container>
  );
}
