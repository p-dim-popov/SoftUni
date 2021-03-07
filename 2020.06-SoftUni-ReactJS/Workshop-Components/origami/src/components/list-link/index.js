import React from 'react'
import styles from './index.module.css'

const ListLink = ({content, href}) => (
    <li className={styles.listItem}>
        <a className={styles.headerLink} href={href}>
            {content}
        </a>
    </li>
)

export default ListLink
