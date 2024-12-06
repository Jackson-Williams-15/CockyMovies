import React, { useState, useEffect, useContext } from 'react';
import { useParams } from 'react-router-dom';
import { getReplies, addReply } from '../../Services/replyService';
import { TextField, Button, Box, Typography } from '@mui/material';
import { AuthContext } from '../../context/AuthContext';

export default function ReviewReplies() {
  const { reviewId } = useParams();
  const { isLoggedIn, username } = useContext(AuthContext);
  const [replies, setReplies] = useState([]);
  const [body, setBody] = useState('');

  useEffect(() => {
    async function fetchReplies() {
      const data = await getReplies(reviewId);
      setReplies(data);
    }
    fetchReplies();
  }, [reviewId]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const replyAuthor = isLoggedIn ? username : 'Anonymous';
    const newReply = { author: replyAuthor, body };    
    await addReply(reviewId, newReply);
    setReplies([...replies, newReply]);
    setBody('');
  };

  return (
    <Box>
      <Typography variant="h6">Replies</Typography>
      {replies.length === 0 ? (
        <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
          There are no replies yet. Be the first to reply!
        </Typography>
      ) : (
        replies.map((reply) => (
          <Box key={reply.id} sx={{ mb: 2 }}>
            <Typography variant="body1"><strong>{reply.author}</strong></Typography>
            <Typography variant="body2">{reply.body}</Typography>
          </Box>
        ))
      )}
        <form onSubmit={handleSubmit}>
          <TextField
            label="Reply"
            value={body}
            onChange={(e) => setBody(e.target.value)}
            fullWidth
            margin="normal"
            multiline
            rows={4}
            required
          />
          <Button type="submit" variant="contained" color="primary" sx={{ mt: 2 }}>
            Add Reply
          </Button>
        </form>
    </Box>
  );
}