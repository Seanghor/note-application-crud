export function formatDate(date: Date | string) {
    return date ? new Date(date).toLocaleDateString() : null
}