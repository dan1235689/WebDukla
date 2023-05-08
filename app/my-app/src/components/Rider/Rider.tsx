import React from 'react';
import { IRider } from '../../models';

export const Rider: React.FunctionComponent<{rider?:IRider}> = (props: {rider?: IRider}) => {

    return (
        props.rider !== undefined
        ?
        <>
        <div className="col-6 col-lg-3 mt-1-9">
            <div className="team-style">
                <div className="team-pic">
                    <img src={props.rider.image ? props.rider.image : "img/team/team1.jpg"} alt="..." />
                </div>

                <div className="team-info px-1 py-3 py-lg-4">
                    <a href="teacher-details.html"><div className="team-name">{props.rider.name}</div></a>
                    <div className="team-position">Age: {props.rider.age}</div>

                    <div className="team-social-info">

                        <ul>
                            <li><a href="#!"><span><i className="fab fa-facebook-f"></i></span></a></li>
                            <li><a href="#!"><span><i className="fab fa-instagram"></i></span></a></li>
                        </ul>

                    </div>

                </div>
            </div>
        </div>
        </>
        :
        <>
        </>
    )
}