using BlazorAppDemo2022.Components;
using BlazorAppDemo2022.Components.Data;

var builder = WebApplication.CreateBuilder(args);

// DI注入
// AddRazorComponentsメソッド(コンポーネントを登録、ルーティング・レンダリング基盤を作る)
// AddInteractiveServerComponentsメソッド(BlazorServerを使用可能に。イベントや状態保持をサーバーで有効化、動的UIが可能になる)
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// DIコンテナに登録
builder.Services.AddScoped<LocalStorageService>();

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
