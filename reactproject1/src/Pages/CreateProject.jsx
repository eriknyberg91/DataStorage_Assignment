import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { createProject, getCustomers, getStatuses, getUsers, getProducts } from "../services/ProjectService";

//All Frontend is created with the help of ChatGPT
const CreateProject = () => {
    const navigate = useNavigate();
    const [formData, setFormData] = useState({
        title: "",
        description: "",
        startDate: "",
        endDate: "",
        customerId: "",
        statusId: "",
        userId: "",
        productId: "",
        projectNumber: "",
    });

    const [customers, setCustomers] = useState([]);
    const [statuses, setStatuses] = useState([]);
    const [users, setUsers] = useState([]);
    const [products, setProducts] = useState([]);
    const [error, setError] = useState("");

    useEffect(() => {
        const fetchData = async () => {
            try {
                setCustomers(await getCustomers());
                setStatuses(await getStatuses());
                setUsers(await getUsers());
                setProducts(await getProducts());
            } catch {
                setError("Error fetching data.");
            }
        };
        fetchData();
    }, []);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleCreateProject = async () => {
        if (!formData.title || !formData.startDate || !formData.endDate || !formData.customerId || !formData.statusId || !formData.userId || !formData.productId || !formData.projectNumber) {
            setError("Please fill in all required fields.");
            return;
        }

        try {
            await createProject(formData);
            navigate("/"); // Redirect to project list
        } catch {
            setError("Error creating project.");
        }
    };

    return (
        <div>
            <button onClick={() => navigate("/")}>Cancel</button>
            <h2>Create Project</h2>
            {error && <p style={{ color: "red" }}>{error}</p>}

            <input type="text" name="title" placeholder="Title" value={formData.title} onChange={handleChange} required />
            <textarea name="description" placeholder="Description" value={formData.description} onChange={handleChange}></textarea>

            <input type="date" name="startDate" value={formData.startDate} onChange={handleChange} required />
            <input type="date" name="endDate" value={formData.endDate} onChange={handleChange} required />

            <select name="customerId" value={formData.customerId} onChange={handleChange} required>
                <option value="">Select Customer</option>
                {customers.map((c) => (
                    <option key={c.id} value={c.id}>{c.customerName}</option>
                ))}
            </select>

            <select name="statusId" value={formData.statusId} onChange={handleChange} required>
                <option value="">Select Status</option>
                {statuses.map((s) => (
                    <option key={s.id} value={s.id}>{s.statusName}</option>
                ))}
            </select>

            <select name="userId" value={formData.userId} onChange={handleChange} required>
                <option value="">Select User</option>
                {users.map((u) => (
                    <option key={u.id} value={u.id}>{u.firstName} {u.lastName}</option>
                ))}
            </select>

            <select name="productId" value={formData.productId} onChange={handleChange} required>
                <option value="">Select Product</option>
                {products.map((p) => (
                    <option key={p.id} value={p.id}>{p.productName}</option>
                ))}
            </select>

            <input type="text" name="projectNumber" placeholder="Project Number" value={formData.projectNumber} onChange={handleChange} required />

            <button onClick={handleCreateProject}>Create</button>
        </div>
    );
};

export default CreateProject;

