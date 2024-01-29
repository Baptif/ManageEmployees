import { ToastContainer } from "react-toastify";
import ReturnButton from "../components/utils/ReturnButton";
import AttendanceList from "../components/attendances/AttendanceList";
import { AttendanceProvider } from "../contexts/AttendanceContext";
import AddAttendance from "../components/attendances/AddAttendance";

const EmployeePage = () => {
  return (
    <>
        <ReturnButton />
        <div className='max-w-4xl mx-auto mt-2'>
          <AttendanceProvider>
            <AddAttendance />
            <AttendanceList/>
          </AttendanceProvider>
        </div>
        <ToastContainer />
    </>
  );
}

export default EmployeePage