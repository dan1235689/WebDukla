import React from 'react';
import { IRider } from '../../models';
import { Rider } from '../Rider';
import { useFetch } from '../../data';

export const Teams: React.FunctionComponent = () => {
    const [riders, setRiders] = React.useState<IRider[]>([]);
    const [categories, setCategories] = React.useState<string[]>([]);
    const data: IRider[] = useFetch("http://localhost:5068/riders");

    React.useEffect(() => {
        setRiders(data);
        setCategories(Array.from(new Set(data.map((r: IRider) => {return r.category as unknown as string}))));
    }, [data]);


    return (
        categories !== undefined
        ?
        <>
            {categories.map((t: string) => {return(
                <section className="bg-light md">
                <div className="container">
                    <div className="row">
                        <div className="col-12 mx-auto mb-6 mb-md-7 text-center">
                            <h2 className="font-weight-700 section-title">Our Team {t}</h2>
                        </div>
                    </div>
    
                    <div className="row mt-n1-9">
                            {riders.filter((r: IRider) => r.category === t).map((rider: IRider) => {
                                return (
                                <Rider key={rider.name} rider={rider} />
                                )
                            })}
                        </div>
    
                </div>
            </section>
            )})}
        </>
        :
        <>
        </>
    )
}