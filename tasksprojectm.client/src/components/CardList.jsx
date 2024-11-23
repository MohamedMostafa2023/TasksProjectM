import React, { useEffect, useState } from 'react';
import {
    Card,
    Text,
    Button,
    Group,
    TextInput,
    Badge,
    ActionIcon,
    Container,
    Modal,
} from '@mantine/core';
import { IconX, IconPencil, IconPlus } from '@tabler/icons-react';

function getAuthTokenFromCookie() {
    const match = document.cookie.match(/(^| )authToken=([^;]+)/);
    return match ? match[2] : null;
}

function CardList({ selectedTaskGroupId, searchQuery }) {
    const [tasks, setTasks] = useState([]);
    const [filteredTasks, setFilteredTasks] = useState([]);
    const [newTitle, setNewTitle] = useState('');
    const [newDescription, setNewDescription] = useState('');
    const [editTaskId, setEditTaskId] = useState(null);
    const [editTaskName, setEditTaskName] = useState('');
    const [editTaskDescription, setEditTaskDescription] = useState('');
    const [editModalOpened, setEditModalOpened] = useState(false);

    const fetchTasks = async (taskGroupId) => {
        const token = getAuthTokenFromCookie();
        try {
            const response = await fetch(`/api/Task/taskgroup/${taskGroupId}`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
            if (!response.ok) {
                throw new Error('Error fetching tasks');
            }
            const data = await response.json();
            setTasks(data);
            setFilteredTasks(data);
        } catch (error) {
            console.error(error);
        }
    };


    useEffect(() => {
        const lowerCaseQuery = searchQuery.toLowerCase();
        setFilteredTasks(
            tasks.filter((task) =>
                task.taskName.toLowerCase().includes(lowerCaseQuery)
            )
        );
    }, [searchQuery, tasks]);


    useEffect(() => {
        if (selectedTaskGroupId) {
            fetchTasks(selectedTaskGroupId);
        }
    }, [selectedTaskGroupId]);


    const addTask = async () => {
        const token = getAuthTokenFromCookie();
        if (newTitle.trim() && newDescription.trim()) {
            try {
                const response = await fetch(`/api/Task`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        Authorization: `Bearer ${token}`,
                    },
                    body: JSON.stringify({
                        taskName: newTitle,
                        taskDescription: newDescription,
                        taskGroupId: selectedTaskGroupId,
                    }),
                });

                if (!response.ok) {
                    throw new Error('Error adding task');
                }

                const newTask = await response.json();
                setTasks([...tasks, newTask]);
                setNewTitle('');
                setNewDescription('');
            } catch (error) {
                console.error(error);
            }
        }
    };


    const openEditModal = (task) => {
        setEditTaskId(task.taskId);
        setEditTaskName(task.taskName);
        setEditTaskDescription(task.taskDescription);
        setEditModalOpened(true);
    };


    const updateTask = async () => {
        const token = getAuthTokenFromCookie();
        if (editTaskName.trim() && editTaskDescription.trim()) {
            try {
                const response = await fetch(`/api/Task/${editTaskId}`, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                        Authorization: `Bearer ${token}`,
                    },
                    body: JSON.stringify({
                        taskId: editTaskId,
                        taskName: editTaskName,
                        taskDescription: editTaskDescription,
                        taskGroupId: selectedTaskGroupId,
                    }),
                });

                if (!response.ok) {
                    throw new Error('Error updating task');
                }

                setTasks((prevTasks) =>
                    prevTasks.map((task) =>
                        task.taskId === editTaskId
                            ? { ...task, taskName: editTaskName, taskDescription: editTaskDescription }
                            : task
                    )
                );
                setEditModalOpened(false);
            } catch (error) {
                console.error(error);
            }
        }
    };


    const deleteTask = async (taskId) => {
        const token = getAuthTokenFromCookie();
        try {
            const response = await fetch(`/api/Task/${taskId}`, {
                method: 'DELETE',
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            if (!response.ok) {
                throw new Error('Error deleting task');
            }

            setTasks((prevTasks) => prevTasks.filter((task) => task.taskId !== taskId));
        } catch (error) {
            console.error(error);
        }
    };

    return (
        <Container style={{ margin: '0 auto', minWidth:'-webkit-fill-available', paddingRight:'0' }}>
            <div style={{ height: 'Calc(100vh - 190px)', overflow: 'auto', width: '100%', padding:'0 20px' }}>
                {/* List of Tasks */}
                {filteredTasks.map((task) => (
                    <Card key={task.taskId} radius="lg" style={{ maxWidth: '40rem', marginLeft: 'auto', marginRight: 'auto' }} shadow="sm" padding="lg" mb="sm">
                        <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                            <Group position="left" style={{ width: '90%', display: 'flex', flexDirection: 'column', alignContent: 'flex-start', alignItems: 'flex-start' }}>
                                <Group>
                                    <Badge gradient={{ from: 'blue', to: 'cyan', deg: 65 }} size="lg" circle style={{ marginBottom: '8px' }}></Badge>
                                    <Text weight={500} size="lg" mb="xs">
                                        {task.taskName}
                                    </Text>
                                </Group>
                            </Group>
                            <Group position="right" style={{ width: '10%', display: 'contents', justifyContent: 'flex-end', alignContent: 'flex-start' }}>
                                <ActionIcon
                                    color="gray"
                                    size="1.3rem"
                                    variant="transparent"
                                    onClick={() => openEditModal(task)}
                                    title="Edit"
                                >
                                    <IconPencil />
                                </ActionIcon>
                                <ActionIcon
                                    color="gray"
                                    size="1.3rem"
                                    variant="transparent"
                                    onClick={() => deleteTask(task.taskId)}
                                    title="Delete"
                                >
                                    <IconX />
                                </ActionIcon>
                            </Group>
                        </div>
                        <Text size="16px" color="dimmed" mt="sm" mb="sm">
                            {task.taskDescription}
                        </Text>
                    </Card>
                ))}
            </div>

            <div style={{ width: '100%' }}>
                {/* Add New Task */}
                <Card shadow="sm" radius="50" style={{ maxWidth: '76%', marginLeft: 'auto', marginRight: 'auto' }} padding="md" mt="md">
                    <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                        <TextInput
                            placeholder="Task Name"
                            value={newTitle}
                            radius="md"
                            variant="unstyled"
                            onChange={(e) => setNewTitle(e.target.value)}
                            style={{ width: '30%' }}
                        />
                        <TextInput
                            placeholder="Task Description"
                            value={newDescription}
                            radius="md"
                            variant="unstyled"
                            onChange={(e) => setNewDescription(e.target.value)}
                            style={{ width: '60%' }}
                        />
                        <Button
                            onClick={addTask}
                            radius="xl"
                            gradient={{ from: 'blue', to: 'cyan', deg: 65 }}
                            style={{
                                padding: '10px',
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
                </Card>
            </div>

            {/* Edit Task Modal */}
            <Modal
                opened={editModalOpened}
                onClose={() => setEditModalOpened(false)}
                title="Edit Task"
                radius="md"
            >
                <TextInput
                    label="Task Name"
                    value={editTaskName}
                    radius="md"
                    onChange={(e) => setEditTaskName(e.target.value)}
                />
                <TextInput
                    label="Task Description"
                    value={editTaskDescription}
                    radius="md"
                    onChange={(e) => setEditTaskDescription(e.target.value)}
                    style={{ marginTop: 10 }}
                />
                <Button
                    onClick={updateTask}
                    radius="md"
                    gradient={{ from: 'blue', to: 'cyan', deg: 65 }}
                    style={{ marginTop: 15 }}
                >
                    Update Task
                </Button>
            </Modal>
        </Container>
    );
}

export default CardList;