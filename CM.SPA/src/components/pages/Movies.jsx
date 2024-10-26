import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getMovies } from '../../Services/movieService';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import CardActions from '@mui/material/CardActions';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
export default function Movies() {
  const [movies, setMovies] = useState([]);

  useEffect(() => {
    const fetchMovies = async () => {
      try {
        const moviesData = await getMovies();
        setMovies(moviesData);
      } catch (error) {
        console.error('Failed to fetch movies:', error);
      }
    };

    fetchMovies();
  }, []);

  return (
    <div>
      <h2>Movies List</h2>
      <Box sx={{ padding: 3 }}>
      <Grid container spacing={3}>
        {movies.map(movie => (
          <Grid item xs={12} sm={6} md={4} key={movie.id}>
            <Card sx={{ height: 350, display: 'flex', flexDirection: 'column' }}>
              <CardMedia
                component="img"
                alt={movie.title}
                height="140"
                image={movie.imageUrl}
              />
              <CardContent sx={{ flexgrow: '1' }}>
                <Typography gutterBottom variant="h5" component="div">
                  {movie.title}
                </Typography>
                <Typography gutterBottom variant="h6" component="div">
                {movie.genres && movie.genres.length > 0
                    ? movie.genres.map(genre => genre.name).join(', ')
                    : 'No genres available'}
                </Typography>
                <Typography variant="body2" 
                ariant="body2" sx={{
                  maxHeight: 80,
                  overflow: 'hidden',
                  textOverflow: 'ellipsis',
                  display: '-webkit-box',
                  WebkitLineClamp: 3,
                  WebkitBoxOrient: 'vertical',
                  color: 'text.secondary'
              }}>
                  {movie.description}
                </Typography>
                {/*<Typography variant="subtitle2" color="text.secondary">
                  {movie.genres && movie.genres.length > 0
                    ? movie.genres.map(genre => genre.name).join(', ')
                    : 'No genres available'}
                </Typography>*/}
              </CardContent>
              <CardActions>
                <Button size="large" component={Link} to={`/movies/${movie.id}/showtimes`}>
                  Show Times
                </Button>
              </CardActions>
            </Card>
          </Grid>
        ))}
      </Grid>
      </Box>
    </div>
  );
}
