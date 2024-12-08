import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Box, Typography, CircularProgress, Alert, Button, FormControl, InputLabel, Select, MenuItem } from '@mui/material';
import MovieForm from './MovieForm';
import ShowtimeForm from './ShowtimeForm';
import EditShowtimeForm from './EditShowtimeForm';
import RemoveShowtimeForm from './RemoveShowtimeForm';
import RemoveMovieForm from './RemoveMovieForm';

const Manager = () => {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [message, setMessage] = useState('');
  const [showMovieForm, setShowMovieForm] = useState(false);
  const [showShowtimeForm, setShowShowtimeForm] = useState(false);
  const [showEditShowtimeForm, setShowEditShowtimeForm] = useState(false);
  const [showRemoveShowtimeForm, setShowRemoveShowtimeForm] = useState(false);
  const [showRemoveMovieForm, setShowRemoveMovieForm] = useState(false);
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
    movieId: '',
    addTickets: 0, // Add this field
    removeTickets: 0, // Add this field
    ticketPrice: 0 // Add this field
  });
  const [removeShowtimeData, setRemoveShowtimeData] = useState({
    movieId: '',
    id: ''
  });
  const [removeMovieData, setRemoveMovieData] = useState({
    id: ''
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
          movieId: showtime.movie.id, // Ensure movieId is set correctly
          addTickets: 0, // Default value for adding tickets
          removeTickets: 0, // Default value for removing tickets
          ticketPrice: showtime.ticketPrice // Default value for ticket price
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

  const handleRemoveShowtimeChange = async (e) => {
    const { name, value } = e.target;
    setRemoveShowtimeData((prevData) => ({
      ...prevData,
      [name]: value,
    }));

    if (name === 'movieId') {
      try {
        const response = await axios.get(`/api/showtimes/movie/${value}`);
        setShowtimes(response.data);
      } catch (error) {
        console.error('Error fetching showtimes:', error);
      }
    }
  };

  const handleRemoveMovieChange = (e) => {
    const { name, value } = e.target;
    setRemoveMovieData((prevData) => ({
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

  const handleEditShowtimeSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.put(`/api/showtimes/${editShowtimeData.id}`, {
        id: editShowtimeData.id,
        startTime: editShowtimeData.startTime,
        capacity: editShowtimeData.capacity,
        movieId: editShowtimeData.movieId, // Include movieId in the payload
      }, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });

      // If addTickets is greater than 0, call the add-to-showtime endpoint
      if (editShowtimeData.addTickets > 0) {
        await axios.post(`/api/tickets/add-to-showtime/${editShowtimeData.id}/${editShowtimeData.addTickets}`, {}, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        });
      }

      // If removeTickets is greater than 0, call the remove-from-showtime endpoint
      if (editShowtimeData.removeTickets > 0) {
        await axios.delete(`/api/tickets/remove-from-showtime/${editShowtimeData.id}/${editShowtimeData.removeTickets}`, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        });
      }

      // Update ticket prices if ticketPrice is set
      if (editShowtimeData.ticketPrice > 0) {
        await axios.put(`/api/tickets/edit-price/${editShowtimeData.id}`, { newPrice: editShowtimeData.ticketPrice }, {
          headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${localStorage.getItem('token')}`,
          },
        });
      }

      setMessage('Showtime edited successfully.');
      setShowEditShowtimeForm(false);
    } catch (error) {
      setError('Error editing showtime. Please try again.');
      console.error('Error editing showtime:', error);
    }
  };

  const handleRemoveShowtimeSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.delete(`/api/showtimes/${removeShowtimeData.id}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });
      setMessage('Showtime removed successfully.');
      setShowRemoveShowtimeForm(false);
    } catch (error) {
      setError('Error removing showtime. Please try again.');
      console.error('Error removing showtime:', error);
    }
  };

  const handleRemoveMovieSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.delete(`/api/movies/${removeMovieData.id}`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem('token')}`,
        },
      });
      setMessage('Movie removed successfully.');
      setShowRemoveMovieForm(false);
    } catch (error) {
      setError('Error removing movie. Please try again.');
      console.error('Error removing movie:', error);
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
      setRemoveShowtimeData((prevData) => ({
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
            movies={movies}
          />
        </>
      )}
      <Button variant="contained" color="primary" onClick={() => setShowRemoveShowtimeForm(!showRemoveShowtimeForm)} sx={{ mt: 2 }}>
        {showRemoveShowtimeForm ? 'Cancel' : 'Remove Showtime'}
      </Button>
      {showRemoveShowtimeForm && (
        <RemoveShowtimeForm
          removeShowtimeData={removeShowtimeData}
          handleRemoveShowtimeChange={handleRemoveShowtimeChange}
          handleRemoveShowtimeSubmit={handleRemoveShowtimeSubmit}
          movies={movies}
          showtimes={showtimes}
        />
      )}
      <Button variant="contained" color="primary" onClick={() => setShowRemoveMovieForm(!showRemoveMovieForm)} sx={{ mt: 2 }}>
        {showRemoveMovieForm ? 'Cancel' : 'Remove Movie'}
      </Button>
      {showRemoveMovieForm && (
        <RemoveMovieForm
          removeMovieData={removeMovieData}
          handleRemoveMovieChange={handleRemoveMovieChange}
          handleRemoveMovieSubmit={handleRemoveMovieSubmit}
          movies={movies}
        />
      )}
    </Box>
  );
};

export default Manager;