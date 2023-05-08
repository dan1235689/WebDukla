import React from "react";
import { IRider } from "../models";

export const useFetch = (url: string): IRider[] => {
    const [data, setData] = React.useState<IRider[]>([]);

    React.useEffect(() => {
        fetch(url)
            .then((res) => res.json())
            .then((data: IRider[]) => setData(data));
    }, [url]);

    return data;
};

