import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import ProjectList from "./pages/ProjectList";
import CreateProject from "./pages/CreateProject";
import EditProject from "./pages/EditProject";

//All Frontend is created with the help of ChatGPT
function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<ProjectList />} />
                <Route path="/projects/create" element={<CreateProject />} />
                <Route path="/projects/edit/:id" element={<EditProject />} />
            </Routes>
        </Router>
    );
}

export default App;