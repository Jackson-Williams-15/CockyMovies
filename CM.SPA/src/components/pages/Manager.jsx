import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Box, Typography, CircularProgress, Alert, TextField, Button, Grid, Select, MenuItem, InputLabel, FormControl, Checkbox, ListItemText } from '@mui/material';

const Manager = () => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [message, setMessage] = useState('');
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState({
    title: '',
    description: '',
    dateReleased: '',
    imageUrl: '',
    ratingId: '',
    genreIds: []
  });
  const [genres, setGenres] = useState([]);
  const [ratings, setRatings] = useState([]);
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

    const fetchGenresAndRatings = async () => {
      try {
        const genresResponse = await axios.get('/api/genre');
        const ratingsResponse = await axios.get('/api/ratings');
        setGenres(genresResponse.data);
        setRatings(ratingsResponse.data);
      } catch (error) {
        console.error('Error fetching genres and ratings:', error);
      }
    };

    fetchManagerData();
    fetchGenresAndRatings();
  }, [navigate]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
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

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post('/api/movies', formData, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });
      setMessage('Movie added successfully.');
      setShowForm(false);
    } catch (error) {
      setError('Error adding movie. Please try again.');
      console.error('Error adding movie:', error);
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
      <Button variant="contained" color="primary" onClick={() => setShowForm(!showForm)} sx={{ mt: 2 }}>
        {showForm ? 'Cancel' : 'Add Movie'}
      </Button>
      {showForm && (
        <Box component="form" onSubmit={handleSubmit} sx={{ mt: 4, width: '100%', maxWidth: 600 }}>
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
    </Box>
  );
};

export default Manager;