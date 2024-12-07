import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Box, Typography, CircularProgress, Alert, TextField, Button, Grid, Select, MenuItem, InputLabel, FormControl, Checkbox, ListItemText } from '@mui/material';

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
        <Box component="form" onSubmit={handleMovieSubmit} sx={{ mt: 4, width: '100%', maxWidth: 600 }}>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <TextField
                label="Title"
                name="title"
                value={formData.title}
                onChange={handleChange}
                fullWidth
                required
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                label="Description"
                name="description"
                value={formData.description}
                onChange={handleChange}
                fullWidth
                required
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                label="Date Released"
                name="dateReleased"
                type="date"
                value={formData.dateReleased}
                onChange={handleChange}
                fullWidth
                InputLabelProps={{
                  shrink: true,
                }}
                required
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                label="Image URL"
                name="imageUrl"
                value={formData.imageUrl}
                onChange={handleChange}
                fullWidth
                required
              />
            </Grid>
            <Grid item xs={12}>
              <FormControl fullWidth required>
                <InputLabel>Rating</InputLabel>
                <Select
                  label="Rating"
                  name="ratingId"
                  value={formData.ratingId}
                  onChange={handleChange}
                >
                  <MenuItem value="">
                    <em>None</em>
                  </MenuItem>
                  {ratings.map((rating) => (
                    <MenuItem key={rating.id} value={rating.id}>
                      {rating.name}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={12}>
              <FormControl fullWidth required>
                <InputLabel>Genres</InputLabel>
                <Select
                  label="Genres"
                  name="genreIds"
                  multiple
                  value={formData.genreIds}
                  onChange={handleGenreChange}
                  renderValue={(selected) => selected.map(id => genres.find(g => g.id === id)?.name).join(', ')}
                >
                  {genres.map((genre) => (
                    <MenuItem key={genre.id} value={genre.id}>
                      <Checkbox checked={formData.genreIds.indexOf(genre.id) > -1} />
                      <ListItemText primary={genre.name} />
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item xs={12}>
              <Button type="submit" variant="contained" color="primary" fullWidth>
                Add Movie
              </Button>
            </Grid>
          </Grid>
        </Box>
      )}
      <Button variant="contained" color="primary" onClick={() => setShowShowtimeForm(!showShowtimeForm)} sx={{ mt: 2 }}>
        {showShowtimeForm ? 'Cancel' : 'Add Showtime'}
      </Button>
      {showShowtimeForm && (
        <Box component="form" onSubmit={handleShowtimeSubmit} sx={{ mt: 4, width: '100%', maxWidth: 600 }}>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <FormControl fullWidth required>
                <InputLabel>Movie</InputLabel>
                <Select
                  label="Movie"
                  name="movieId"
                  value={showtimeData.movieId}
                  onChange={handleShowtimeChange}
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
            </Grid>
            <Grid item xs={12}>
              <TextField
                label="Start Time"
                name="startTime"
                type="datetime-local"
                value={showtimeData.startTime}
                onChange={handleShowtimeChange}
                fullWidth
                InputLabelProps={{
                  shrink: true,
                }}
                required
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                label="Capacity"
                name="capacity"
                type="number"
                value={showtimeData.capacity}
                onChange={handleShowtimeChange}
                fullWidth
                required
              />
            </Grid>
            <Grid item xs={12}>
              <Button type="submit" variant="contained" color="secondary" fullWidth>
                Add Showtime
              </Button>
            </Grid>
          </Grid>
        </Box>
      )}
    </Box>
  );
};

export default Manager;