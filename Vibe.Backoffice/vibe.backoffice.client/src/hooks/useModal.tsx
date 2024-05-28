import { Dispatch, SetStateAction, useState } from "react";

export default function useModal<T>(initialState: T | null | (() => T | null) = null): [value: T | null, show: Dispatch<SetStateAction<T | null>>, hide: () => void] {
	const [value, setValue] = useState<T | null>(initialState);
	const hide = () => setValue(null);

	return [value, setValue, hide];
}