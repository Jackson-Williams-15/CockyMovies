import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import {
  Card,
  CardContent,
  Typography,
  Grid,
  Box,
  CircularProgress,
} from '@mui/material';
import userService from '../../Services/userService';

const Profile = () => {
  const { username } = useParams();
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const userData = await userService.getUserInfo(username);
        setUser(userData);
      } catch (error) {
        setError('Error fetching user data');
        console.error('Error fetching user data:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchUser();
  }, [username]);

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
        <Typography color="error">{error}</Typography>
      </Box>
    );
  }

  return (
    <Box sx={{ display: 'flex', justifyContent: 'center', marginTop: 4 }}>
      <Card sx={{ maxWidth: 500, width: '100%', padding: 2, boxShadow: 3 }}>
        <Grid container spacing={2} alignItems="center">
          <Grid item></Grid>
          <Grid item xs>
            <Typography variant="h5" component="div">
              {user?.username}'s Profile
            </Typography>
          </Grid>
        </Grid>
        <CardContent>
          <Typography variant="subtitle1" color="text.secondary">
            Email:
          </Typography>
          <Typography variant="body1">{user?.email}</Typography>

          <Box sx={{ mt: 2 }}>
            <Typography variant="subtitle1" color="text.secondary">
              Date of Birth:
            </Typography>
            <Typography variant="body1">
              {new Date(user?.dateOfBirth).toLocaleDateString() || 'N/A'}
            </Typography>
          </Box>
        </CardContent>
      </Card>
    </Box>
  );
};

export default Profile;
