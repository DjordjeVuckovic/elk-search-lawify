const missText = "Start to search for highlights...🔬";
const missTextv2 = "Back to search for highlights...🔬";
const stringSplitFn = (title: string | undefined): string[] | undefined => title?.split(/\s+/);
export const highlightLightShort = (text?: string) => {
    if (!text) {
        return missText;
    }
    const words = stringSplitFn(text);
    let shortTitle = words ? words!.slice(0, 10).join(' ') : missText;
    if (words && words.length > 20) {
        shortTitle += ' ...';
    }
    return `<span>${highlightColored(shortTitle)}</span>`;
}

export function highlightColored(htmlString?: string) {
    if (!htmlString) {
        return missTextv2
    }
    // Define the replacement tag with the desired styles
    const replacementStartTag = '<span style="background-color: #ff922d;">';
    const replacementEndTag = '</span>';

    // Replace <em> start tags with the replacement start tag
    return htmlString?.replace(/<em>/g, replacementStartTag)
        .replace(/<\/em>/g, replacementEndTag);
}