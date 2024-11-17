import {
    TextInput,
    PasswordInput,
    Paper,
    Title,
    Container,
    Button,
    Text,
} from '@mantine/core';
import { useNavigate } from 'react-router-dom';
import { useState } from 'react';
import './Login.css';

function Login() {
    const [userName, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(false);
    const [token, setToken] = useState('');

    const saveTokenToCookie = (token) => {
        const expires = new Date();
        expires.setTime(expires.getTime() + 24 * 60 * 60 * 1000); // Token valid for 1 day
        //expires.setTime(expires.getTime() + 1 * 60 * 1000); // Token valid for 1 minute
        document.cookie = `authToken=${token}; expires=${expires.toUTCString()}; path=/; Secure; SameSite=Strict`;
    };

    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError('');
        setToken('');

        try {
            const response = await fetch('/api/Auth/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    userName,
                    password,
                }),
            });

            if (!response.ok) {
                throw new Error('Invalid username or password');
            }

            const data = await response.json();

            if (data.token) {
                setToken(data.token);
                saveTokenToCookie(data.token);
                navigate('/');
            } else {
                throw new Error('Token not found in response');
            }
        } catch (error) {
            setError(error.message || 'Something went wrong');
        } finally {
            setLoading(false);
        }
    };

    return (
        <Container size={420} radius="md" my={40}>
            <Title ta="center">Welcome back!</Title>

            <Paper withBorder shadow="md" p={30} mt={30} radius="md">
                <form onSubmit={handleLogin}>
                    <TextInput
                        label="Username"
                        placeholder="Your username"
                        required
                        radius="md"
                        value={userName}
                        onChange={(e) => setUsername(e.target.value)}
                    />
                    <PasswordInput
                        label="Password"
                        placeholder="Your password"
                        required
                        mt="md"
                        radius="md"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                    {error && <Text color="red" mt="sm">{error}</Text>}
                    <Button fullWidth mt="xl" radius="md" type="submit" loading={loading}>
                        Sign in
                    </Button>
                </form>
            </Paper>
        </Container>
    );
}

export default Login;