import { Container, Grid, Skeleton, Text } from '@mantine/core';
import React, { useEffect , useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './App.css';
import SideNav from './components/SideNav';
import AppHeader from './components/AppHeader';
import CardList from './components/CardList';

function getAuthTokenFromCookie() {
    const match = document.cookie.match(/(^| )authToken=([^;]+)/);
    return match ? match[2] : null;
}

function isTokenExpired(token) {
    try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        const now = Math.floor(Date.now() / 1000);
        return payload.exp && payload.exp < now;
    } catch (error) {
        console.error('Invalid token:', error);
        return true;
    }
}

function App() {
    const navigate = useNavigate();

    const [selectedTaskGroupId, setSelectedTaskGroupId] = useState(null);
    const [searchQuery, setSearchQuery] = useState('');
    const [isSideNavVisible, setIsSideNavVisible] = useState(true);

    const handleSearch = (query) => {
        setSearchQuery(query);
    };

    const toggleSideNav = () => {
        setIsSideNavVisible((prev) => !prev);
    };

    useEffect(() => {
        const token = getAuthTokenFromCookie();
        if (!token || isTokenExpired(token)) {
            navigate('/login');
        }
    }, [navigate]);

    return (
        <Container fluid h={50} my="md">
            <Grid gutter="md">
                <Grid.Col className="customSmooth" style={{ backgroundColor: '#f7f9fc', height: '95vh', width: '18.7rem', position: 'absolute', zIndex: '1000', marginLeft: isSideNavVisible ? '0rem' : '-21rem' }}>
                    <SideNav onTaskGroupSelect={setSelectedTaskGroupId} />
                </Grid.Col>
                <Grid.Col className="customSmooth" style={{ paddingLeft: isSideNavVisible ? '20rem' : '1rem' }}>
                    <AppHeader onSearch={handleSearch} onToggleSideNav={toggleSideNav} />
                    <CardList selectedTaskGroupId={selectedTaskGroupId} searchQuery={searchQuery} />
                </Grid.Col>
            </Grid>
        </Container>
    );
}

export default App;
