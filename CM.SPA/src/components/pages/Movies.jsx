import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getMovies, getGenres, getRatings } from '../../Services/movieService';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import CardActions from '@mui/material/CardActions';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import CircularProgress from '@mui/material/CircularProgress';
import Container from '@mui/material/Container';
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import Checkbox from '@mui/material/Checkbox';
import ListItemText from '@mui/material/ListItemText';
import Rating from '@mui/material/Rating';

export default function Movies() {
  const [movies, setMovies] = useState([]);
  const [loading, setLoading] = useState(true);
  const [genres, setGenres] = useState([]);
  const [ratings, setRatings] = useState([]);
  const [selectedGenres, setSelectedGenres] = useState([]);
  const [selectedRatings, setSelectedRatings] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [moviesData, genresData, ratingsData] = await Promise.all([
          getMovies(),
          getGenres(),
          getRatings(),
        ]);
        setMovies(moviesData);
        setGenres(genresData);
        setRatings(ratingsData);
        setLoading(false);
      } catch (error) {
        console.error('Failed to fetch data:', error);
        setLoading(false);
      }
    };
    fetchData();
  }, []);

  const handleGenreChange = (event) => {
    setSelectedGenres(event.target.value);
  };

  const handleRatingChange = (event) => {
    setSelectedRatings(event.target.value);
  };

  const fetchFilteredMovies = async () => {
    try {
      const moviesData = await getMovies(selectedGenres, selectedRatings);
      setMovies(moviesData);
    } catch (error) {
      console.error('Failed to fetch movies:', error);
    }
  };

  useEffect(() => {
    fetchFilteredMovies();
  }, [selectedGenres, selectedRatings]);

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

  return (
    <Container sx={{ mt: 4, mb: 4 }}>
      <Typography variant="h4" component="h1" gutterBottom>
        Movies List
      </Typography>
      <FormControl sx={{ m: 1, minWidth: 120 }}>
        <InputLabel>Genres</InputLabel>
        <Select
          multiple
          value={selectedGenres}
          onChange={handleGenreChange}
          renderValue={(selected) => selected.join(', ')}
        >
          {genres.map((genre) => (
            <MenuItem key={genre.id} value={genre.id}>
              <Checkbox checked={selectedGenres.indexOf(genre.id) > -1} />
              <ListItemText primary={genre.name} />
            </MenuItem>
          ))}
        </Select>
      </FormControl>
      <FormControl sx={{ m: 1, minWidth: 120 }}>
        <InputLabel>Ratings</InputLabel>
        <Select
          multiple
          value={selectedRatings}
          onChange={handleRatingChange}
          renderValue={(selected) => selected.join(', ')}
        >
          {ratings.map((rating) => (
            <MenuItem key={rating.id} value={rating.id}>
              <Checkbox checked={selectedRatings.indexOf(rating.id) > -1} />
              <ListItemText primary={rating.name} />
            </MenuItem>
          ))}
        </Select>
      </FormControl>
      <Box sx={{ padding: 3 }}>
        <Grid container spacing={3}>
          {movies.map((movie) => (
            <Grid item xs={12} sm={6} md={4} key={movie.id}>
              <Card
                sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}
              >
                <CardMedia
                  component="img"
                  alt={movie.title}
                  height="140"
                  image={movie.imageUrl}
                />
                <CardContent sx={{ flexGrow: 1 }}>
                  <Typography gutterBottom variant="h5" component="div">
                    {movie.title}
                  </Typography>
                  <Typography gutterBottom variant="h6" component="div">
                    {movie.genres && movie.genres.length > 0
                      ? movie.genres.map((genre) => genre.name).join(', ')
                      : 'No genres available'}
                  </Typography>
                  {movie.averageReviewRating !== null && (
                    <Box sx={{ display: 'flex', alignItems: 'center', mb: 1 }}>
                      <Rating value={movie.averageReviewRating} readOnly precision={0.5} />
                      <Typography variant="body2" sx={{ ml: 1 }}>
                        {movie.averageReviewRating.toFixed(1)}
                      </Typography>
                    </Box>
                  )}
                  <Typography
                    variant="body2"
                    sx={{
                      maxHeight: 80,
                      overflow: 'hidden',
                      textOverflow: 'ellipsis',
                      display: '-webkit-box',
                      WebkitLineClamp: 3,
                      WebkitBoxOrient: 'vertical',
                      color: 'text.secondary',
                    }}
                  >
                    {movie.description}
                  </Typography>
                </CardContent>
                <CardActions>
                  <Button
                    size="large"
                    component={Link}
                    to={`/movies/${movie.id}/showtimes`}
                  >
                    Show Times
                  </Button>
                  <Button
                    size="large"
                    component={Link}
                    to={`/movies/${movie.id}/reviews`}
                  >
                    Reviews
                  </Button>
                </CardActions>
              </Card>
            </Grid>
          ))}
        </Grid>
      </Box>
    </Container>
  );
}