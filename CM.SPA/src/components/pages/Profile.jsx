import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import {
  Card,
  CardContent,
  Typography,
  Grid,
  Box,
  CircularProgress,
  TextField,
  Button,
  Alert,
} from '@mui/material';
import userService from '../../Services/userService';

const Profile = () => {
  const { username } = useParams();
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [passwordError, setPasswordError] = useState(null);
  const [formData, setFormData] = useState({
    email: '',
    username: '',
    dateOfBirth: '',
    oldPassword: '',
    newPassword: '',
  });

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const userData = await userService.getUserInfo();
        setUser(userData);
        setFormData({
          email: userData.email,
          username: userData.username,
          dateOfBirth: new Date(userData.dateOfBirth)
            .toISOString()
            .split('T')[0],
        });
      } catch (error) {
        setError('Error fetching user data');
        console.error('Error fetching user data:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchUser();
  }, [username]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const updatedUser = await userService.updateUserInfo(formData);
      alert('Profile updated successfully');
    } catch (error) {
      setError(error.response.data.message || 'Error updating profile');
      console.error('Error updating profile:', error);
    }
  };

  const handlePasswordChange = async (e) => {
    e.preventDefault();
    try {
      const passwordData = {
        oldPassword: formData.oldPassword,
        newPassword: formData.newPassword,
      };
      const response = await userService.updatePassword(passwordData);
      alert(response.message);
      setFormData((prevData) => ({
        ...prevData,
        oldPassword: '',
        newPassword: '',
      }));
      setPasswordError(null);
    } catch (error) {
      setPasswordError(
        error.response.data.message || 'Error updating password',
      );
      console.error('Error updating password:', error);
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
          <form onSubmit={handleSubmit}>
            <TextField
              label="Email"
              name="email"
              value={formData.email}
              onChange={handleChange}
              fullWidth
              margin="normal"
            />
            <TextField
              label="Username"
              name="username"
              value={formData.username}
              onChange={handleChange}
              fullWidth
              margin="normal"
            />
            <TextField
              label="Date of Birth"
              name="dateOfBirth"
              type="date"
              value={formData.dateOfBirth}
              onChange={handleChange}
              fullWidth
              margin="normal"
              InputLabelProps={{
                shrink: true,
              }}
            />
            {error && (
              <Alert severity="error" sx={{ mt: 2 }}>
                {error}
              </Alert>
            )}
            <Box sx={{ mt: 2 }}>
              <Button type="submit" variant="contained" color="primary">
                Save Changes
              </Button>
            </Box>
          </form>
          <form onSubmit={handlePasswordChange} style={{ marginTop: '20px' }}>
            <TextField
              label="Old Password"
              name="oldPassword"
              type="password"
              value={formData.oldPassword}
              onChange={handleChange}
              fullWidth
              margin="normal"
            />
            <TextField
              label="New Password"
              name="newPassword"
              type="password"
              value={formData.newPassword}
              onChange={handleChange}
              fullWidth
              margin="normal"
            />
            {passwordError && (
              <Alert severity="error" sx={{ mt: 2 }}>
                {passwordError}
              </Alert>
            )}
            <Box sx={{ mt: 2 }}>
              <Button type="submit" variant="contained" color="secondary">
                Change Password
              </Button>
            </Box>
          </form>
        </CardContent>
      </Card>
    </Box>
  );
};

export default Profile;
