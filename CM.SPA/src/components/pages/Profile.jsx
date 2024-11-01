import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
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
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  if (!user) {
    return <div>User not found</div>;
  }

  return (
    <div>
      <h1>{user.username}'s Profile</h1>
      <p>Email: {user.email}</p>
      <p>Date of Birth: {user.dateOfBirth}</p>
    </div>
  );
};

export default Profile;
