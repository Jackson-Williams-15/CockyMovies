import React, { useEffect, useState, useContext } from 'react';
import { useParams } from 'react-router-dom';
import { getReviews } from '../../Services/reviewService';
import { getMovieById } from '../../Services/movieService';
import { AuthContext } from '../../context/AuthContext';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import CircularProgress from '@mui/material/CircularProgress';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Divider from '@mui/material/Divider';
import Rating from '@mui/material/Rating';
import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';
import ReviewForm from './ReviewForm';

export default function Reviews() {
  const { movieId } = useParams();
  const [movie, setMovie] = useState(null);
  const { isLoggedIn, username } = useContext(AuthContext);
  const [reviews, setReviews] = useState([]);
  const [loading, setLoading] = useState(true);
  const [editingReview, setEditingReview] = useState(null);
  const [addingReview, setAddingReview] = useState(false);

  useEffect(() => {
    const fetchReviews = async () => {
      try {
        const reviewsData = await getReviews(movieId);
        setReviews(reviewsData);
      } catch (error) {
        console.error('Failed to fetch reviews:', error);
      } finally {
        setLoading(false);
      }
    };

    const fetchMovie = async () => {
      try {
        const movieData = await getMovieById(movieId);
        setMovie(movieData);
      } catch (error) {
        console.error('Failed to fetch movie:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchReviews();
    fetchMovie();
  }, [movieId]);

  const handleAddReviewSubmit = async (newReview) => {
    try {
      await addReview(newReview);
      setReviews((prevReviews) => [...prevReviews, newReview]);
      setAddingReview(false);
    } catch (error) {
      console.error('Failed to add review:', error);
    }
  };

  const handleAddReviewClick = () => {
    setAddingReview(true);
  };

  const handleEditClick = (review) => {
    setEditingReview(review);
  };

  const handleEditSubmit = async (updatedReview) => {
    // Call your API to update the review
    try {
      await editReview(updatedReview.id, updatedReview);
      setReviews((prevReviews) =>
        prevReviews.map((review) =>
          review.id === updatedReview.id ? updatedReview : review
        )
      );
      setEditingReview(null);
    } catch (error) {
      console.error('Failed to update review:', error);
    }
  };

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
    <Container sx={{ mt: 6, mb: 6 }}>
      {movie && (
        <Typography variant="h3" component="h1" gutterBottom align="center">
          Reviews for <span style={{ color: '#d32f2f' }}>{movie.title}</span>
        </Typography>
      )}
      <Divider sx={{ mb: 4 }} />
      {isLoggedIn ? (
        !addingReview && (
          <Button variant="contained" color="primary" onClick={handleAddReviewClick}>
            Add Review
          </Button>
        )
      ) : (
        <Typography variant="body2" color="error" gutterBottom>
          You must be logged in to add a review.
        </Typography>
      )}
      {addingReview && (
        <ReviewForm
          onSubmit={handleAddReviewSubmit}
          onCancel={() => setAddingReview(false)}
        />
      )}
      <Divider sx={{ mb: 4 }} />
      <Grid container spacing={4}>
        {reviews.map((review) => (
          <Grid item xs={12} sm={6} md={4} key={review.id}>
            <Card sx={{ height: '100%' }}>
              <CardContent>
                <Typography variant="h5" gutterBottom>
                  {review.title}
                </Typography>
                <Typography variant="subtitle2" color="text.secondary">
                  by {review.username ? review.username : 'Anonymous'}
                </Typography>
                <Divider sx={{ my: 2 }} />
                <Rating value={review.rating} readOnly precision={0.5} />
                <Typography
                  variant="body2"
                  color="text.secondary"
                  sx={{ mt: 1 }}
                >
                  {review.description}
                </Typography>
                {isLoggedIn && review.username === username && (
                  <Button onClick={() => handleEditClick(review)}>Edit</Button>
                )}
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>
      {editingReview && (
        <ReviewForm
          review={editingReview}
          onSubmit={handleEditSubmit}
          onCancel={() => setEditingReview(null)}
        />
      )}
    </Container>
  );
}