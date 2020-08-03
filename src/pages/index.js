import React from "react"
import ExtLink from "../components/extLink"
import { Link } from "gatsby"

export default function Home() {
    return (
        <div>
            <h1>Hello!</h1>
            <p>
                <img className="avatar" src="Cory.png" alt="ReverentGeek Style Avatar of Cory" />
            </p>
            <h2>I'm Cory</h2>
            <p>
                I do computer things with the computers. Experienced in
                automating things with PowerShell. Are you hiring? I'm{" "}
                <Link to="/hireme">available for hire.</Link>
            </p>
            <ul>
                <li>
                    <ExtLink
                        icon="https://twitter.com/favicon.ico"
                        href="https://twitter.com/coryknox"
                        text="Twitter"
                    />
                </li>
                <li>
                    <ExtLink 
                        icon="https://github.com/favicon.ico"
href="https://github.com/corbob" text="GitHub" />
                </li>
                <li>
                    <ExtLink
                        icon="https://linkedin.com/favicon.ico"
                        href="https://linkedin.com/in/knoxcory"
                        text="LinkedIn"
                    />
                </li>
            </ul>
        </div>
    )
}
