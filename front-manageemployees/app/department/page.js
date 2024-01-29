import { ToastContainer } from "react-toastify";
import AddDepartement from "../components/departments/AddDepartement";
import DepartmentList from "../components/departments/DepartmentList";
import ReturnButton from "../components/utils/ReturnButton";
import { DepartmentProvider } from "../contexts/DepartmentContext";

const DepartmentPage = () => {
  return (
    <>
        <ReturnButton/>
        <div className='max-w-4xl mx-auto mt-2'>
            <DepartmentProvider>
                <AddDepartement />
                <DepartmentList />
            </DepartmentProvider>
        </div>
        <ToastContainer />
    </>
  );
}

export default DepartmentPage