import React from 'react';
import { Link } from 'react-router-dom';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardActions from '@mui/material/CardActions';

export default function Home() {
  return (
    <Container sx={{ mt: 8, mb: 4 }}>
      <Box sx={{ textAlign: 'center' }}>
        <Typography variant="h2" component="h1" gutterBottom>
          Welcome to Cocky Movies
        </Typography>
        <Typography variant="h5" component="p" color="text.secondary" sx={{ mb: 4 }}>
          Discover the latest movies, view showtimes, and book your tickets all in one place.
        </Typography>
        
        <Card
          sx={{
            maxWidth: 600,
            margin: 'auto',
            boxShadow: 3,
            borderRadius: 2,
          }}
        >
          <CardContent>
            <Typography variant="h6" component="div" gutterBottom>
              Explore Now
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Browse our selection of movies, find the perfect showtime, and secure your seats with ease.
            </Typography>
          </CardContent>
          <CardActions sx={{ justifyContent: 'center' }}>
            <Button
              size="large"
              variant="contained"
              color="primary"
              component={Link}
              to="/movies"
            >
              View Movies
            </Button>
          </CardActions>
        </Card>
      </Box>
    </Container>
  );
}
