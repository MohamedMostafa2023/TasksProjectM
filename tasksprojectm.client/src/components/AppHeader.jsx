import { useState, useEffect } from 'react';
import {
    Burger,
    Text,
    TextInput,
    Avatar,
    Menu,
    UnstyledButton,
    Group,
} from '@mantine/core';
import { IconSearch, IconLogout, IconChevronDown } from '@tabler/icons-react';
import userAvatar from '../assets/bird-thumbnail.jpg';

function AppHeader({ onSearch, onToggleSideNav }) {
    const [searchValue, setSearchValue] = useState('');
    const user = { name: 'Dev Mohamed', avatar: '../assets/bird-thumbnail.jpg' };

    const handleLogout = () => {
        document.cookie = 'authToken=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
        window.location.href = '/login';
    };

    const handleSearchChange = (value) => {
        setSearchValue(value);
        onSearch(value);
    };

    return (
        <div px="md" style={{ display: 'flex', alignItems: 'center', height: '65px' }}>
            <Burger
                opened={false}
                onClick={() => onToggleSideNav((o) => !o)}
                size="sm"
                mr="xl"
                aria-label="Toggle navigation"
            />

            <h3 size="lg" weight={700} style={{ marginRight: '50px' }}>Tasks</h3>

            <TextInput
                placeholder="Search tasks..."
                leftSection={<IconSearch size={16} />}
                value={searchValue}
                radius="md"
                onChange={(event) => handleSearchChange(event.target.value)}
                style={{ width: 'Calc(100% - 390px)', marginRight: 'auto' }}
            />

            <Menu shadow="md" width={200}>
                <Menu.Target>
                    <UnstyledButton>
                        <Group spacing="xs">
                            <Avatar src={userAvatar} radius="xl" size="md" />
                            <Text weight={500} size="sm">
                                {user.name}
                            </Text>
                            <IconChevronDown size={16} />
                        </Group>
                    </UnstyledButton>
                </Menu.Target>

                <Menu.Dropdown>
                    <Menu.Item icon={<IconLogout size={14} />} onClick={handleLogout}>
                        Logout
                    </Menu.Item>
                </Menu.Dropdown>
            </Menu>
        </div>
    );
}

export default AppHeader;
