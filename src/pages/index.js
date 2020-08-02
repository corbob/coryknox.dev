import React from "react"
import ExtLink from "../components/extLink"
import { Link } from "gatsby"

export default function Home() {
    return (
        <div style={{'text-align': `center`}}>
            <h1>Cory Knox</h1>
            <p>
                <img
                    src="Cory.png"
                    style={{ width: "30%" }}
                    alt="ReverentGeek Style Avatar of Cory"
                />
            </p>
            <ul>
                <li>
                    <ExtLink href="https://twitter.com/coryknox" text="Twitter" />
                </li>
                <li>
                    <ExtLink href="https://github.com/corbob" text="GitHub" />
                </li>
                <li>
                    <ExtLink href="https://linkedin.com/in/knoxcory" text="LinkedIn" />
                </li>
        <li>
        <Link to="/hireme">Resume</Link>
        </li>
            </ul>
        </div>
    )
}
