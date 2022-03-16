
export const setTheme = (newTheme) => {
    localStorage.theme = newTheme
}

export const setSystemTheme = () => {
    localStorage.removeItem('theme')
}