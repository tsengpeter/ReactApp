import { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { getPersoninfo, addPersoninfo, deletePersoninfo, updatePersoninfo } from './api/api_personinfo';
import './App.css';

function App() {
    const [personinfos, setPersoninfos] = useState([]);
    const { register, handleSubmit, reset, setValue } = useForm();

    const fetchPersoninfo = async () => {
        try {
            const data = await getPersoninfo({});
            setPersoninfos(data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    useEffect(() => {
        fetchPersoninfo();
    }, []);

    const onSubmit = async (data) => {
        if (data.operation === 'add') {
            await handleAdd(data);
        } else if (data.operation === 'update') {
            await handleUpdate(data);
        }
        reset();
    };

    const handleAdd = async (data) => {
        try {
            const response = await addPersoninfo({
                Name: data.Name,
                Phone: data.Phone,
                Note: data.Note
            });
            if (response.Cmd === 0) {
                fetchPersoninfo();
            } else {
                alert(response.Message);
            }
        } catch (error) {
            console.error('Error adding data:', error);
        }
    };

    const handleUpdate = async (data) => {
        try {
            const response = await updatePersoninfo({
                No: data.No,
                Name: data.Name,
                Phone: data.Phone,
                Note: data.Note
            });
            if (response.Cmd === 0) {
                fetchPersoninfo();
            } else {
                alert(response.Message);
            }
        } catch (error) {
            console.error('Error updating data:', error);
        }
    };

    const handleDelete = async (no, name) => {
        try {
            const response = await deletePersoninfo({ No: no, Name: name });
            if (response.Cmd === 0) {
                fetchPersoninfo();
            } else {
                alert(response.Message);
            }
        } catch (error) {
            console.error('Error deleting data:', error);
        }
    };

    const editArea = () => {
        return (
            <>
                <div>
                    <h3>Add / Update Personinfo</h3>
                    <form onSubmit={handleSubmit(onSubmit)}>
                        <input type="hidden" {...register('operation')} />
                        <div>
                            <label>No:</label>
                            <input type="text" {...register('No')} />
                        </div>
                        <div>
                            <label>Name:</label>
                            <input type="text" {...register('Name', { required: 'Name is required' })} />
                        </div>
                        <div>
                            <label>Phone:</label>
                            <input type="text" {...register('Phone', { required: 'Phone is required' })} />
                        </div>
                        <div>
                            <label>Note:</label>
                            <input type="text" {...register('Note')} />
                        </div>
                        <div>
                            <button type="button" onClick={() => { setValue('operation', 'add'); handleSubmit(onSubmit)(); }}>Add</button>
                            <button type="button" onClick={() => { setValue('operation', 'update'); handleSubmit(onSubmit)(); }}>Update</button>
                        </div>
                    </form>
                </div>
            </>
        );
    };

    const contents = () => {
        return (
            <>
                <div>
                    <h3>Personinfo List</h3>
                    <table>
                        <thead>
                            <tr>
                                <th>No</th>
                                <th>Name</th>
                                <th>Phone</th>
                                <th>Note</th>
                                <th>Actions</th>
                            </tr>
                        </thead>
                        <tbody>
                            {personinfos.map((person) => (
                                <tr key={person.no}>
                                    <td>{person.no}</td>
                                    <td>{person.name}</td>
                                    <td>{person.phone}</td>
                                    <td>{person.note}</td>
                                    <td>
                                        <button onClick={() => handleDelete(person.no, person.name)}>Delete</button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </>
        );
    };

    return (
        <>
            <div>
                <h2>Personinfo CRUD</h2>
                {editArea()}
                {contents()}
            </div>
        </>
    );
}

export default App;
