// Load Google Model Viewer web component library
(function() {
    console.log('[ModelViewer] Script initialization started');
    
    // Load the CDN script
    const script = document.createElement('script');
    script.type = 'module';
    script.src = 'https://cdn.jsdelivr.net/npm/@google/model-viewer@3.0.1/dist/model-viewer.min.js';
    script.crossOrigin = 'anonymous';
    
    script.onload = function() {
        console.log('[ModelViewer] CDN script loaded successfully');
        window.ModelViewerReady = true;
        // Dispatch custom event so other code knows it's ready
        document.dispatchEvent(new CustomEvent('modelViewerReady'));
    };
    
    script.onerror = function(err) {
        console.error('[ModelViewer] Failed to load CDN script:', err);
    };
    
    // Add to head immediately
    document.head.appendChild(script);
    console.log('[ModelViewer] CDN script element added to document head');
    
})();
