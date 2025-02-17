import { useState, useEffect } from "react";
import PropTypes from "prop-types";
import { createProject, getCustomers, getStatuses, getUsers, getProducts } from "../Services/ProjectService";

const ProjectComponent = ({ onProjectCreated }) => {
    const initialFormData = {
        title: "",
        description: "",
        startDate: "",
        endDate: "",
        customerId: "",
        statusId: "",
        userId: "",
        productId: "",
        projectNumber: "",
    };

    const [formData, setFormData] = useState(initialFormData);
    const [customers, setCustomers] = useState([]);
    const [statuses, setStatuses] = useState([]);
    const [users, setUsers] = useState([]);
    const [products, setProducts] = useState([]);
    const [error, setError] = useState("");
    const [successMessage, setSuccessMessage] = useState("");

    useEffect(() => {
        const fetchData = async () => {
            try {
                setCustomers(await getCustomers());
                setStatuses(await getStatuses());
                setUsers(await getUsers());
                setProducts(await getProducts());
            } catch (error) {
                setError("Error fetching data.");
                console.log(error);
            }
        };
        fetchData();
    }, []);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleCreateProject = async () => {
        setError("");
        setSuccessMessage("");

        if (!formData.title || !formData.startDate || !formData.endDate || !formData.customerId || !formData.statusId || !formData.userId || !formData.productId || !formData.projectNumber) {
            setError("Please fill in all required fields.");
            return;
        }

        try {
            const formattedData = {
                ...formData,
                customerId: parseInt(formData.customerId),
                statusId: parseInt(formData.statusId),
                userId: parseInt(formData.userId),
                productId: parseInt(formData.productId),
                startDate: new Date(formData.startDate).toISOString(),
                endDate: new Date(formData.endDate).toISOString(),
            };

            const createdProject = await createProject(formattedData);
            setSuccessMessage(`Project created successfully: ${createdProject.title}`);

            setFormData(initialFormData); // ✅ Reset all fields after successful project creation
        } catch (error) {
            setError(error.message || "An error occurred.");
        }
    };

    // ✅ Trigger project list update AFTER formData resets
    useEffect(() => {
        if (successMessage) {
            onProjectCreated();
        }
    }, [successMessage, onProjectCreated]);

    return (
        <div>
            <h2>Create a New Project</h2>
            {error && <p style={{ color: "red" }}>{error}</p>}
            {successMessage && <p style={{ color: "green" }}>{successMessage}</p>}

            <input type="text" name="title" placeholder="Project Title" value={formData.title} onChange={handleChange} required />
            <input type="text" name="description" placeholder="Description" value={formData.description} onChange={handleChange} />
            <input type="date" name="startDate" value={formData.startDate} onChange={handleChange} required />
            <input type="date" name="endDate" value={formData.endDate} onChange={handleChange} required />

            <select name="customerId" value={formData.customerId} onChange={handleChange} required>
                <option value="">Select Customer</option>
                {customers.map((customer) => (
                    <option key={customer.id} value={customer.id}>
                        {customer.name || customer.customerName || "Unnamed Customer"}
                    </option>
                ))}
            </select>

            <select name="statusId" value={formData.statusId} onChange={handleChange} required>
                <option value="">Select Status</option>
                {statuses.map((status) => (
                    <option key={status.id} value={status.id}>
                        {status.name || status.statusName || "Unnamed Status"}
                    </option>
                ))}
            </select>

            <select name="userId" value={formData.userId} onChange={handleChange} required>
                <option value="">Select User</option>
                {users.map((user) => (
                    <option key={user.id} value={user.id}>
                        {user.firstName && user.lastName
                            ? `${user.firstName} ${user.lastName}`
                            : user.firstName || user.lastName || "Unnamed User"}
                    </option>
                ))}
            </select>

            <select name="productId" value={formData.productId} onChange={handleChange} required>
                <option value="">Select Product</option>
                {products.map((product) => (
                    <option key={product.id} value={product.id}>
                        {product.name || product.productName || "Unnamed Product"}
                    </option>
                ))}
            </select>

            <input type="text" name="projectNumber" placeholder="Project Number" value={formData.projectNumber} onChange={handleChange} required />

            <button onClick={handleCreateProject}>Create Project</button>
        </div>
    );
};

ProjectComponent.propTypes = {
    onProjectCreated: PropTypes.func.isRequired,
};

export default ProjectComponent;

