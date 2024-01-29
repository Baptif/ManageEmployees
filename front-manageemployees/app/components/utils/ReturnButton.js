import Link from "next/link";
import { FiArrowLeft } from "react-icons/fi";

const ReturnButton = () => {
  return (
    <Link href="/" className="flex items-center mt-2 ml-2">
        <FiArrowLeft className="mr-2"/> Retour
    </Link>
  );
}

export default ReturnButton