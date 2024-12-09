import React, { useEffect, useState, useContext } from 'react';
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
import { AuthContext } from '../../context/AuthContext';

const Profile = () => {
  const { username } = useParams();
  const [user, setUser] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [passwordError, setPasswordError] = useState(null);
  const [successMessage, setSuccessMessage] = useState(null);
  const [formData, setFormData] = useState({
    email: '',
    username: '',
    dateOfBirth: '',
    oldPassword: '',
    newPassword: '',
    cardNumber: '',
    expiryDate: '',
    cvv: '',
    cardHolderName: '',
    paymentMethod: 'Credit Card',
  });
  const { updateUser } = useContext(AuthContext);

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
          cardNumber: userData.paymentDetails?.cardNumber || '',
          expiryDate: userData.paymentDetails?.expiryDate || '',
          cvv: userData.paymentDetails?.cvv || '',
          cardHolderName: userData.paymentDetails?.cardHolderName || '',
          paymentMethod:
            userData.paymentDetails?.paymentMethod || 'Credit Card',
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
      setSuccessMessage('Profile updated successfully');
      setError(null);

      // Update user information in AuthContext
      updateUser(updatedUser);
    } catch (error) {
      setError(error.response.data.message || 'Error updating profile');
      setSuccessMessage(null);
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

  const handleSavePaymentDetails = async (e) => {
    e.preventDefault();
    setSuccessMessage(null);
    setError(null);

    // Validate card number (16 digits)
    if (!/^\d{16}$/.test(formData.cardNumber.trim())) {
      setError('Card number must be exactly 16 digits.');
      return;
    }

    // Validate CVV (3-4 digits)
    if (!/^\d{3,4}$/.test(formData.cvv.trim())) {
      setError('CVV must be 3 or 4 digits.');
      return;
    }

    // Validate expiry date (MM/YY format and not in the past)
    const [month, year] = formData.expiryDate.split('/');
    const expiryDate = new Date(`20${year}-${month}-01`);
    const currentDate = new Date();
    if (
      !/^(0[1-9]|1[0-2])\/?([0-9]{2})$/.test(formData.expiryDate) ||
      expiryDate < currentDate
    ) {
      setError('Expiry date is invalid or in the past.');
      return;
    }

    try {
      const existingUser = await userService.getUserInfo();
      const existingPaymentDetails = existingUser?.paymentDetails || {};

      // Compare each field to detect changes
      const isCardNumberChanged =
        (existingPaymentDetails.cardNumber || '').trim() !==
        formData.cardNumber.trim();
      const isExpiryDateChanged =
        (existingPaymentDetails.expiryDate || '').trim() !==
        formData.expiryDate.trim();
      const isCVVChanged =
        (existingPaymentDetails.cvv || '').trim() !== formData.cvv.trim();
      const isCardHolderNameChanged =
        (existingPaymentDetails.cardHolderName || '').trim().toLowerCase() !==
        formData.cardHolderName.trim().toLowerCase();
      const isPaymentMethodChanged =
        (existingPaymentDetails.paymentMethod || '').trim().toLowerCase() !==
        formData.paymentMethod.trim().toLowerCase();

      // If no changes detected
      if (
        !isCardNumberChanged &&
        !isExpiryDateChanged &&
        !isCVVChanged &&
        !isCardHolderNameChanged &&
        !isPaymentMethodChanged
      ) {
        setSuccessMessage('No changes detected in payment details.');
        setError(null);
      } else {
        // Proceed to save if changes are detected
        await userService.savePaymentDetails({
          cardNumber: formData.cardNumber.trim(),
          expiryDate: formData.expiryDate.trim(),
          cvv: formData.cvv.trim(),
          cardHolderName: formData.cardHolderName.trim(),
          paymentMethod: formData.paymentMethod.trim(),
        });

        setSuccessMessage('Payment details updated successfully.');
        setError(null);
      }
    } catch (error) {
      const errorMessage =
        error.response?.data?.message || 'Error saving payment details.';
      setError(errorMessage);
      setSuccessMessage(null);
      console.error('Error saving payment details:', error);
    }
  };

  if (loading) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', marginTop: 4 }}>
        <CircularProgress />
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
          {successMessage && (
            <Alert severity="success" sx={{ mt: 2 }}>
              {successMessage}
            </Alert>
          )}
          {error && (
            <Alert severity="error" sx={{ mt: 2 }}>
              {error}
            </Alert>
          )}
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
          <form
            onSubmit={handleSavePaymentDetails}
            style={{ marginTop: '20px' }}
          >
            <TextField
              label="Card Number"
              name="cardNumber"
              value={formData.cardNumber}
              onChange={handleChange}
              fullWidth
              margin="normal"
            />
            <TextField
              label="Expiry Date"
              name="expiryDate"
              value={formData.expiryDate}
              onChange={handleChange}
              fullWidth
              margin="normal"
            />
            <TextField
              label="CVV"
              name="cvv"
              value={formData.cvv}
              onChange={handleChange}
              fullWidth
              margin="normal"
            />
            <TextField
              label="Card Holder Name"
              name="cardHolderName"
              value={formData.cardHolderName}
              onChange={handleChange}
              fullWidth
              margin="normal"
            />
            <TextField
              label="Payment Method"
              name="paymentMethod"
              value={formData.paymentMethod}
              onChange={handleChange}
              fullWidth
              margin="normal"
            />
            <Box sx={{ mt: 2 }}>
              <Button type="submit" variant="contained" color="secondary">
                Save Payment Details
              </Button>
            </Box>
          </form>
        </CardContent>
      </Card>
    </Box>
  );
};

export default Profile;
