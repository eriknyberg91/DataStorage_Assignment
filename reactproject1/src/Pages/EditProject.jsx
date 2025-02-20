import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { getProjectById, updateProject, getCustomers, getStatuses, getUsers, getProducts } from "../services/ProjectService";

//All Frontend is created with the help of ChatGPT
const EditProject = () => {
    const { id } = useParams();
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
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const fetchProject = async () => {
            try {
                const projectData = await getProjectById(id);
                if (projectData) {
                    setFormData({
                        title: projectData.title || "",
                        description: projectData.description || "",
                        startDate: projectData.startDate ? projectData.startDate.split("T")[0] : "",
                        endDate: projectData.endDate ? projectData.endDate.split("T")[0] : "",
                        customerId: projectData.customerId || "",
                        statusId: projectData.statusId || "",
                        userId: projectData.userId || "",
                        productId: projectData.productId || "",
                        projectNumber: projectData.projectNumber || "",
                    });
                } else {
                    setError("Project not found.");
                }
            } catch (err) {
                setError("Failed to fetch project.");
            } finally {
                setLoading(false);
            }
        };

        const fetchData = async () => {
            try {
                setCustomers(await getCustomers());
                setStatuses(await getStatuses());
                setUsers(await getUsers());
                setProducts(await getProducts());
            } catch {
                setError("Error fetching dropdown data.");
            }
        };

        fetchProject();
        fetchData();
    }, [id]);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleUpdateProject = async () => {
        try {
            await updateProject(id, formData);
            alert("Project updated successfully!");
            navigate("/"); // Redirect after update
        } catch {
            setError("Failed to update project.");
        }
    };

    if (loading) return <p>Loading...</p>;
    if (error) return <p style={{ color: "red" }}>{error}</p>;

    return (
        <div>
            <button onClick={() => navigate("/")}>Cancel</button>
            <h2>Edit Project</h2>
            {error && <p style={{ color: "red" }}>{error}</p>}

            <input type="text" name="title" value={formData.title} onChange={handleChange} required />
            <textarea name="description" value={formData.description} onChange={handleChange}></textarea>

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

            <input type="text" name="projectNumber" value={formData.projectNumber} onChange={handleChange} required />

            <button onClick={handleUpdateProject}>Update Project</button>
        </div>
    );
};

export default EditProject;


