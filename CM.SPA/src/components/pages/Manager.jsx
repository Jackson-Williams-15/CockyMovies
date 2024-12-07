import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Box, Typography, CircularProgress, Alert, Button } from '@mui/material';
import MovieForm from './MovieForm';
import ShowtimeForm from './ShowtimeForm';

const Manager = () => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [message, setMessage] = useState('');
  const [showMovieForm, setShowMovieForm] = useState(false);
  const [showShowtimeForm, setShowShowtimeForm] = useState(false);
  const [formData, setFormData] = useState({
    title: '',
    description: '',
    dateReleased: '',
    imageUrl: '',
    ratingId: '',
    genreIds: []
  });
  const [showtimeData, setShowtimeData] = useState({
    movieId: '',
    startTime: '',
    capacity: ''
  });
  const [genres, setGenres] = useState([]);
  const [ratings, setRatings] = useState([]);
  const [movies, setMovies] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchManagerData = async () => {
      try {
        const response = await axios.get('/api/manager/dashboard', {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        });
        setMessage(response.data.message);
      } catch (error) {
        setError('You do not have access to this page.');
        navigate('/'); // Redirect to home if not authorized
      } finally {
        setLoading(false);
      }
    };

    const fetchGenresRatingsMovies = async () => {
      try {
        const genresResponse = await axios.get('/api/genre');
        const ratingsResponse = await axios.get('/api/ratings');
        const moviesResponse = await axios.get('/api/movies');
        setGenres(genresResponse.data);
        setRatings(ratingsResponse.data);
        setMovies(moviesResponse.data);
      } catch (error) {
        console.error('Error fetching genres, ratings, and movies:', error);
      }
    };

    fetchManagerData();
    fetchGenresRatingsMovies();
  }, [navigate]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleShowtimeChange = (e) => {
    const { name, value } = e.target;
    setShowtimeData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleGenreChange = (e) => {
    const { value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      genreIds: value,
    }));
  };

  const handleMovieSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post('/api/movies', formData, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });
      setMessage('Movie added successfully.');
      setShowMovieForm(false);
    } catch (error) {
      setError('Error adding movie. Please try again.');
      console.error('Error adding movie:', error);
    }
  };

  const handleShowtimeSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post('/api/showtimes', showtimeData, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });
      setMessage('Showtime added successfully.');
      setShowShowtimeForm(false);
    } catch (error) {
      setError('Error adding showtime. Please try again.');
      console.error('Error adding showtime:', error);
    }
  };

  if (loading) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', marginTop: 4 }}>
        <CircularProgress />
      </Box>
    );
  }

  if (error) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', marginTop: 4 }}>
        <Alert severity="error">{error}</Alert>
      </Box>
    );
  }

  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', marginTop: 4 }}>
      <Typography variant="h4">{message}</Typography>
      <Button variant="contained" color="primary" onClick={() => setShowMovieForm(!showMovieForm)} sx={{ mt: 2 }}>
        {showMovieForm ? 'Cancel' : 'Add Movie'}
      </Button>
      {showMovieForm && (
        <MovieForm
          formData={formData}
          handleChange={handleChange}
          handleGenreChange={handleGenreChange}
          handleMovieSubmit={handleMovieSubmit}
          genres={genres}
          ratings={ratings}
        />
      )}
      <Button variant="contained" color="primary" onClick={() => setShowShowtimeForm(!showShowtimeForm)} sx={{ mt: 2 }}>
        {showShowtimeForm ? 'Cancel' : 'Add Showtime'}
      </Button>
      {showShowtimeForm && (
        <ShowtimeForm
          showtimeData={showtimeData}
          handleShowtimeChange={handleShowtimeChange}
          handleShowtimeSubmit={handleShowtimeSubmit}
          movies={movies}
        />
      )}
    </Box>
  );
};

export default Manager;