import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Box, Typography, CircularProgress, Alert, Button, FormControl, InputLabel, Select, MenuItem } from '@mui/material';
import MovieForm from './MovieForm';
import ShowtimeForm from './ShowtimeForm';
import EditShowtimeForm from './EditShowtimeForm';

const Manager = () => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [message, setMessage] = useState('');
  const [showMovieForm, setShowMovieForm] = useState(false);
  const [showShowtimeForm, setShowShowtimeForm] = useState(false);
  const [showEditShowtimeForm, setShowEditShowtimeForm] = useState(false);
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
  const [editShowtimeData, setEditShowtimeData] = useState({
    id: '',
    startTime: '',
    capacity: '',
    movieId: ''
  });
  const [selectedMovieId, setSelectedMovieId] = useState('');
  const [genres, setGenres] = useState([]);
  const [ratings, setRatings] = useState([]);
  const [movies, setMovies] = useState([]);
  const [showtimes, setShowtimes] = useState([]);
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

  const handleEditShowtimeChange = async (e) => {
    const { name, value } = e.target;
    if (name === 'id') {
      try {
        const response = await axios.get(`/api/showtimes/${value}`);
        const showtime = response.data;
        setEditShowtimeData({
          id: showtime.id,
          startTime: showtime.startTime,
          capacity: showtime.capacity,
          movieId: showtime.movieId
        });
      } catch (error) {
        console.error('Error fetching showtime details:', error);
      }
    } else {
      setEditShowtimeData((prevData) => ({
        ...prevData,
        [name]: value,
      }));
    }
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

  const handleEditShowtimeSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.put(`/api/showtimes/${editShowtimeData.id}`, editShowtimeData, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });
      setMessage('Showtime edited successfully.');
      setShowEditShowtimeForm(false);
    } catch (error) {
      setError('Error editing showtime. Please try again.');
      console.error('Error editing showtime:', error);
    }
  };

  const handleMovieSelectChange = async (e) => {
    const movieId = e.target.value;
    setSelectedMovieId(movieId);
    try {
      const response = await axios.get(`/api/showtimes/movie/${movieId}`);
      setShowtimes(response.data);
      setEditShowtimeData((prevData) => ({
        ...prevData,
        movieId: movieId
      }));
    } catch (error) {
      console.error('Error fetching showtimes:', error);
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
      <Button variant="contained" color="primary" onClick={() => setShowEditShowtimeForm(!showEditShowtimeForm)} sx={{ mt: 2 }}>
        {showEditShowtimeForm ? 'Cancel' : 'Edit Showtime'}
      </Button>
      {showEditShowtimeForm && (
        <>
          <FormControl fullWidth required sx={{ mt: 2 }}>
            <InputLabel>Movie</InputLabel>
            <Select
              label="Movie"
              value={selectedMovieId}
              onChange={handleMovieSelectChange}
            >
              <MenuItem value="">
                <em>None</em>
              </MenuItem>
              {movies.map((movie) => (
                <MenuItem key={movie.id} value={movie.id}>
                  {movie.title}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
          <EditShowtimeForm
            editShowtimeData={editShowtimeData}
            handleEditShowtimeChange={handleEditShowtimeChange}
            handleEditShowtimeSubmit={handleEditShowtimeSubmit}
            showtimes={showtimes}
          />
        </>
      )}
    </Box>
  );
};

export default Manager;