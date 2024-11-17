import { useEffect, useState } from 'react';
import { IconCheckbox, IconX, IconPencil, IconPlus } from '@tabler/icons-react';
import {
    Box,
    NavLink,
    ActionIcon,
    Group,
    Button,
    TextInput,
    List,
    Text,
    Modal,
} from '@mantine/core';

function getAuthTokenFromCookie() {
    const match = document.cookie.match(/(^| )authToken=([^;]+)/);
    return match ? match[2] : null;
}

function Sidebar({ onTaskGroupSelect }) {
    const [active, setActive] = useState(null);
    const [items, setItems] = useState([]);
    const [newItemName, setNewItemName] = useState('');
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [currentRenameId, setCurrentRenameId] = useState(null);
    const [renameInput, setRenameInput] = useState('');
    const token = getAuthTokenFromCookie();

    const handleTaskGroupClick = (id, index) => {
        setActive(index);
        onTaskGroupSelect(id);
    };


    const fetchTaskGroups = async () => {
        try {
            const response = await fetch('/api/TaskGroup', {
                method: 'GET',
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            if (!response.ok) {
                throw new Error(`Error fetching task groups: ${response.statusText}`);
            }

            const data = await response.json();
            setItems(
                data.map((group) => ({
                    id: group.taskGroupId,
                    label: group.taskGroupName,
                    icon: IconCheckbox,
                }))
            );
        } catch (error) {
            console.error(error);
        }
    };

    useEffect(() => {
        fetchTaskGroups();
    }, []);


    const addItem = async () => {
        if (!newItemName.trim()) {
            return;
        }

        try {
            const response = await fetch('/api/TaskGroup', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({ taskGroupName: newItemName }),
            });

            if (!response.ok) {
                throw new Error(`Error adding task group: ${response.statusText}`);
            }

            const data = await response.json();
            setItems([...items, { id: data.taskGroupId, label: newItemName, icon: IconCheckbox }]);
            setNewItemName('');
        } catch (error) {
            console.error(error);
        }
    };


    const renameItem = async () => {
        if (!renameInput.trim() || currentRenameId === null) {
            return;
        }

        try {
            const response = await fetch(`/api/TaskGroup/${currentRenameId}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({ taskGroupId: currentRenameId , taskGroupName: renameInput }),
            });

            if (!response.ok) {
                throw new Error(`Error renaming task group: ${response.statusText}`);
            }

            setItems(items.map((item) => (item.id === currentRenameId ? { ...item, label: renameInput } : item)));
            setIsModalOpen(false);
            setRenameInput('');
        } catch (error) {
            console.error(error);
        }
    };


    const deleteItem = async (id) => {
        try {
            const response = await fetch(`/api/TaskGroup/${id}`, {
                method: 'DELETE',
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            if (!response.ok) {
                throw new Error(`Error deleting task group: ${response.statusText}`);
            }

            setItems(items.filter((item) => item.id !== id));
        } catch (error) {
            console.error(error);
        }
    };


    const openRenameModal = (id, currentName) => {
        setCurrentRenameId(id);
        setRenameInput(currentName);
        setIsModalOpen(true);
    };


    const itemsList = items.map((item, index) => (
        <NavLink
            href="#required-for-focus"
            key={item.id}
            active={index === active}
            style={{ borderRadius: '8px' }}
            label={item.label}
            rightSection={
                <Group spacing="xs">
                    <ActionIcon
                        color="#8caac4"
                        size="1.3rem"
                        variant="transparent"
                        onClick={() => openRenameModal(item.id, item.label)}
                    >
                        <IconPencil />
                    </ActionIcon>
                    <ActionIcon color="#8caac4" size="1.3rem" variant="transparent" onClick={() => deleteItem(item.id)}>
                        <IconX />
                    </ActionIcon>
                </Group>
            }
            leftSection={<item.icon size="1.15rem" stroke={1.5} />}
            onClick={() => handleTaskGroupClick(item.id, index)}
        />
    ));

    return (
        <Box p="md" style={{ backgroundColor: '#f7f9fc', height: '95vh' }}>
            <h3 style={{ textAlign: 'center' }}>Task Groups</h3>
            <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '1rem' }}>
                <TextInput
                    value={newItemName}
                    onChange={(e) => setNewItemName(e.target.value)}
                    style={{ width: '80%' }}
                    placeholder="Enter task group name"
                    radius="md"
                    size="sm"
                />
                <Button
                    onClick={addItem}
                    radius="xl"
                    gradient={{ from: 'blue', to: 'cyan', deg: 65 }}
                    style={{
                        marginLeft: '10px',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        width: '35px',
                        height: '35px',
                    }}
                >
                    <IconPlus size={20} />
                </Button>
            </div>
            <List spacing="xs" style={{ padding: 0 }}>
                {itemsList}
            </List>

            {/* Rename Modal */}
            <Modal
                opened={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                title="Rename Task Group"
                radius="md"
                centered
            >
                <TextInput
                    value={renameInput}
                    onChange={(e) => setRenameInput(e.target.value)}
                    placeholder="Enter new name"
                    radius="md"
                />
                <Group position="right" mt="md">
                    <Button onClick={() => setIsModalOpen(false)} radius="md" variant="outline">
                        Cancel
                    </Button>
                    <Button onClick={renameItem} variant="filled" radius="md" gradient={{ from: 'blue', to: 'cyan' }}>
                        Save
                    </Button>
                </Group>
            </Modal>
        </Box>
    );
}

export default Sidebar;