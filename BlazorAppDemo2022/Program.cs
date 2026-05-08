using BlazorAppDemo2022.Components;
using BlazorAppDemo2022.Components.Service;
using Microsoft.AspNetCore.Components.Server;

var builder = WebApplication.CreateBuilder(args);

// DI注入
// AddRazorComponentsメソッド(コンポーネントを登録、ルーティング・レンダリング基盤を作る)
// AddInteractiveServerComponentsメソッド(BlazorServerを使用可能に。イベントや状態保持をサーバーで有効化、動的UIが可能になる)
builder.Services.AddRazorComponents().AddInteractiveServerComponents().AddHubOptions(options =>
{
    // クライアントが応答しなくなってから切断と判断するまでの時間
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);

    // サーバーがクライアントにPing送る間隔
    options.KeepAliveInterval = TimeSpan.FromSeconds(15);

    // ハンドシェイクタイムアウト
    options.HandshakeTimeout = TimeSpan.FromSeconds(30);
});

// Circuit自体の保持時間
builder.Services.Configure<CircuitOptions>(options =>
{
    // 切断後、再接続できる猶予
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(30); // 規定値3分

    // 保持するCircuit数（負荷対策）
    options.DisconnectedCircuitMaxRetained = 200; // 規定値100
});

// カート機能想定
builder.Services.AddScoped<CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// HTTPをHTTPSに強制リダイレクト 
app.UseHttpsRedirection();

// 静的ファイルの配信設定
app.UseStaticFiles();

// CSRF対策
app.UseAntiforgery();

// ルーティング登録,インタラクティブ機能の登録
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
