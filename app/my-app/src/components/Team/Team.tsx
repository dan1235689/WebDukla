import React from 'react';
import { IRider } from '../../models';
import { Rider } from '../Rider';

export const Team: React.FunctionComponent<{team?:IRider[]}> = (props: {team?: IRider[]}) => {
    return (
        props.team !== undefined
        ?
        <>
        <section className="bg-light md">
            <div className="container">
                <div className="row">
                    <div className="col-12 mx-auto mb-6 mb-md-7 text-center">
                        <h2 className="font-weight-700 section-title">Our Team U23</h2>
                    </div>
                </div>

                <div className="row mt-n1-9">
                        {props.team.map((rider: IRider) => {
                            return (
                            <Rider key={rider.name} rider={rider} />
                            )
                        })}
                    </div>

            </div>
        </section>
        </>
        :
        <>
        </>
    )
}