import { ToastContainer } from "react-toastify";
import AddEmployee from "../components/employees/AddEmployee";
import EmployeeList from "../components/employees/EmployeeList";
import ReturnButton from "../components/utils/ReturnButton";
import { EmployeeProvider } from "../contexts/EmployeeContext";

const EmployeePage = () => {
  return (
    <>
        <ReturnButton />
        <div className='max-w-4xl mx-auto mt-2'>
            <EmployeeProvider>
                <AddEmployee />
                <EmployeeList />
            </EmployeeProvider>
        </div>
        <ToastContainer />
    </>
  );
}

export default EmployeePage