import React from "react";

export default function ExtLink(props) {
    return <a href={props.href}><img src={props.icon} alt="" /> {props.text}</a>
}
