import React, { useState, useEffect } from 'react';

const Cart = () => {
  // Initialize state for cart items
  const [cartItems, setCartItems] = useState([]);
  const [total, setTotal] = useState(0);
  const [isCheckingOut, setIsCheckingOut] = useState(false);
  const [checkoutMessage, setCheckoutMessage] = useState('');

  // Fetch cart items from API or initialize with dummy data
  useEffect(() => {
    // Dummy data to simulate fetching from an API
    const initialCartItems = [
      {
        ticketId: 1,
        movieTitle: 'Movie A',
        showtime: '7:00 PM',
        quantity: 2,
        price: 12.5,
      },
      {
        ticketId: 2,
        movieTitle: 'Movie B',
        showtime: '9:00 PM',
        quantity: 1,
        price: 15.0,
      },
    ];

    setCartItems(initialCartItems);
  }, []);

  // Update total price whenever the cart items change
  useEffect(() => {
    const totalPrice = cartItems.reduce((acc, item) => acc + item.quantity * item.price, 0);
    setTotal(totalPrice);
  }, [cartItems]);

  // Update quantity for a specific ticket
  const updateQuantity = (ticketId, newQuantity) => {
    setCartItems((prevItems) =>
      prevItems.map((item) =>
        item.ticketId === ticketId ? { ...item, quantity: newQuantity } : item
      )
    );
  };

  // Remove a ticket from the cart
  const removeTicket = (ticketId) => {
    setCartItems((prevItems) => prevItems.filter((item) => item.ticketId !== ticketId));
  };

  // Simulate the checkout process
  const handleCheckout = () => {
    if (cartItems.length === 0) {
      setCheckoutMessage("Your cart is empty. Add items before checking out.");
      return;
    }

    setIsCheckingOut(true);
    setCheckoutMessage("");

    // Simulate payment processing
    setTimeout(() => {
      // Simulate successful payment and clear the cart
      setCartItems([]);
      setIsCheckingOut(false);
      setCheckoutMessage("Checkout successful! Thank you for your purchase.");
    }, 2000); // Simulates a 2 second delay for checkout process
  };

  return (
    <div className="cart-container">
      <h2>Your Cart</h2>
      {cartItems.length === 0 ? (
        <p>Your cart is empty.</p>
      ) : (
        <>
          <table className="cart-table">
            <thead>
              <tr>
                <th>Movie</th>
                <th>Showtime</th>
                <th>Quantity</th>
                <th>Price</th>
                <th>Total</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {cartItems.map((item) => (
                <tr key={item.ticketId}>
                  <td>{item.movieTitle}</td>
                  <td>{item.showtime}</td>
                  <td>
                    <input
                      type="number"
                      value={item.quantity}
                      min="1"
                      onChange={(e) => updateQuantity(item.ticketId, parseInt(e.target.value, 10))}
                    />
                  </td>
                  <td>${item.price.toFixed(2)}</td>
                  <td>${(item.quantity * item.price).toFixed(2)}</td>
                  <td>
                    <button onClick={() => removeTicket(item.ticketId)}>Remove</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          <div className="cart-total">
            <h3>Total: ${total.toFixed(2)}</h3>
          </div>
          <button className="checkout-button" onClick={handleCheckout} disabled={isCheckingOut}>
            {isCheckingOut ? 'Processing...' : 'Proceed to Checkout'}
          </button>
          {checkoutMessage && <p className="checkout-message">{checkoutMessage}</p>}
        </>
      )}
    </div>
  );
};

export default Cart;
