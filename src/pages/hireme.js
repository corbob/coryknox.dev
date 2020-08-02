import React from "react"
import { Link } from "gatsby"
import ExtLink from "../components/extLink"

export default function Hireme() {
    return (
        <div style={{'text-align': `center`}}>
        <h1> Cory Knox - Resume</h1>
        <p>This is where a resume will be once I finish it. For now you can find it here: <ExtLink href="https://registry.jsonresume.org/corbob" text="json resume" />.</p>
        <Link to="/">Home</Link>
        </div>
    )
}

