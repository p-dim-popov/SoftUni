import React from 'react'
import styles from './index.module.css'
import ListLink from "../list-link";

const Header = () => (
    <header className={styles.navigation}>
        <ul>
            <ListLink href={'#'} content={'going to 1'}/>
            <ListLink href={'#'} content={'going to 1'}/>
            <ListLink href={'#'} content={'going to 1'}/>
            <ListLink href={'#'} content={'going to 1'}/>
        </ul>
    </header>
)

export default Header
