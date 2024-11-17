import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { MantineProvider, createTheme } from '@mantine/core';
import '@mantine/core/styles.css';
import './index.css';
import Login from './Login';
import App from './App';

// Utility to check for token in cookies
function getAuthTokenFromCookie() {
    const match = document.cookie.match(/(^| )authToken=([^;]+)/);
    return match ? match[2] : null;
}

// Custom PrivateRoute component to enforce authorization
function PrivateRoute({ children }) {
    const token = getAuthTokenFromCookie();
    return token ? children : <Navigate to="/login" replace />;
}

const theme = createTheme({
    // Define your theme here
});

createRoot(document.getElementById('root')).render(
    <MantineProvider theme={theme}>
        <StrictMode>
            <Router>
                <Routes>
                    {/* Protect the App route */}
                    <Route
                        path="/"
                        element={
                            <PrivateRoute>
                                <App />
                            </PrivateRoute>
                        }
                    />
                    {/* Public login route */}
                    <Route path="/login" element={<Login />} />
                </Routes>
            </Router>
        </StrictMode>
    </MantineProvider>
);